using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.ViewModels.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StriveOwner.Android.Helper
{
    public abstract class MySwipeHelper : ItemTouchHelper.SimpleCallback
    {
        int buttonWidth, swipePosition = -1;
        float swipeThreshold = 0.5f;
        Dictionary<int, List<MyButton>> buttonBuffer;
        Queue<int> removeQueu = new Queue<int>();
        GestureDetector.SimpleOnGestureListener gestureListener;
        View.IOnTouchListener onTouchListener;
        RecyclerView recyclerView;
        List<MyButton> buttonList;
        CheckOutViewModel checkOut;
        GestureDetector gestureDetector;
        CheckoutDetails CheckoutDetails;
        public abstract void InstantiateMyButton(RecyclerView.ViewHolder viewHolder, List<MyButton> buffer);

        public MySwipeHelper(Context context, RecyclerView recyclerView, int buttonWidth, CheckoutDetails checkoutDetails) : base(0, ItemTouchHelper.Left)
        {
            this.recyclerView = recyclerView;
            this.buttonList = new List<MyButton>();
            this.buttonBuffer = new Dictionary<int, List<MyButton>>();
            this.buttonWidth = buttonWidth;
            this.CheckoutDetails = checkoutDetails;

            gestureListener = new MyGestureListener(this);
            onTouchListener = new MyOnTouchListener(this);

            this.gestureDetector = new GestureDetector(context, gestureListener);
            this.recyclerView.SetOnTouchListener(onTouchListener);

            AttachSwipe();
        }

        private void AttachSwipe()
        {
            ItemTouchHelper itemTouchHelper = new ItemTouchHelper(this);
            itemTouchHelper.AttachToRecyclerView(recyclerView);
        }

        private class MyGestureListener : GestureDetector.SimpleOnGestureListener
        {
            private MySwipeHelper mySwipeHelper;

            public MyGestureListener(MySwipeHelper mySwipeHelper)
            {
                this.mySwipeHelper = mySwipeHelper;
            }

            public override bool OnSingleTapUp(MotionEvent e)
            {
                foreach (MyButton button in mySwipeHelper.buttonList)
                {
                    if (button.OnClick(e.GetX(), e.GetY()))
                        break;
                }
                return true;
            }
        }

        private class MyOnTouchListener : Java.Lang.Object, View.IOnTouchListener
        {
            private MySwipeHelper mySwipeHelper;

            public MyOnTouchListener(MySwipeHelper mySwipeHelper)
            {
                this.mySwipeHelper = mySwipeHelper;
            }

            public bool OnTouch(View v, MotionEvent e)
            {
                if (mySwipeHelper.swipePosition < 0) return false;
                Point point = new Point((int)e.RawX, (int)e.RawY);

                RecyclerView.ViewHolder viewHolder = mySwipeHelper.recyclerView.FindViewHolderForAdapterPosition(mySwipeHelper.swipePosition);
                View itemSwiped = viewHolder.ItemView;
                Rect rect = new Rect();
                itemSwiped.GetGlobalVisibleRect(rect);

                if (e.Action == MotionEventActions.Down || e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Move)
                {
                    if (rect.Top < point.Y && rect.Bottom > point.Y)
                        mySwipeHelper.gestureDetector.OnTouchEvent(e);
                    else
                    {
                        mySwipeHelper.removeQueu.Enqueue(mySwipeHelper.swipePosition);
                        mySwipeHelper.swipePosition = -1;
                        mySwipeHelper.RecoverSwipedItem();

                    }
                }
                return false;
            }
        }

        private void RecoverSwipedItem()
        {
            while (removeQueu.Count > 0)
            {
                int pos = removeQueu.Dequeue();
                if (pos > -1)
                    recyclerView.GetAdapter().NotifyItemChanged(pos);
            }
        }

        //Override

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
        {
            return false;
        }

        public override float GetSwipeThreshold(RecyclerView.ViewHolder viewHolder)
        {
            return swipeThreshold;
        }

        public override float GetSwipeEscapeVelocity(float defaultValue)
        {
            return 0.1f + defaultValue;
        }

        public override float GetSwipeVelocityThreshold(float defaultValue)
        {
            return 5.0f * defaultValue;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            int pos = viewHolder.AdapterPosition;
            if (swipePosition != pos)
            {
                if (!removeQueu.Contains(swipePosition))
                    removeQueu.Enqueue(swipePosition);
                swipePosition = pos;
                if (buttonBuffer.ContainsKey(swipePosition))
                    buttonList = buttonBuffer[swipePosition];
                else
                    buttonList.Clear();
                buttonBuffer.Clear();
                swipeThreshold = 0.5f * buttonList.Count * buttonWidth;
                RecoverSwipedItem();
            }
        }

        public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            int pos = viewHolder.AdapterPosition;
            float translationX = dX;
            View itemView = viewHolder.ItemView;
            if (pos < 0)
            {
                swipePosition = pos;
                return;
            }
            if (actionState == ItemTouchHelper.ActionStateSwipe)
            {
                if (dX < 0)
                {
                    List<MyButton> buffer = new List<MyButton>();
                    if (!buttonBuffer.ContainsKey(pos))
                    {
                        InstantiateMyButton(viewHolder, buffer);
                        buttonBuffer.Add(pos, buffer);
                    }
                    else
                    {
                        buffer = buttonBuffer[pos];
                    }
                    translationX = dX * buffer.Count * buttonWidth / itemView.Width;
                    DrawButton(c, itemView, buffer, pos, translationX);
                }
            }
            base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
        }

        private void DrawButton(Canvas c, View itemView, List<MyButton> buffer, int pos, float translationX)
        {
            float right = itemView.Right;
            float dButtonWidth = -1 * translationX / buffer.Count;
            foreach (MyButton button in buffer)
            {
                float left = right - dButtonWidth;
                button.OnDraw(c, new RectF(left, itemView.Top, right, itemView.Bottom), pos);
                right = left;
            }
        }
    }
}