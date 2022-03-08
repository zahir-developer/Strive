using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Graphics.Drawables;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.TimInventory;
using StriveOwner.Android.Resources.Fragments;

namespace StriveOwner.Android.Helper
{
    public class MyButton
    {
        
        private int imageResId, textSize, pos;
        private string text, color;
        private RectF clickRegion;
        private MyButtonClickListener listener;
        private DeleteButtonClickListener deleteListener;
        private Context context;
        private checkOutViewModel checkOut;
        private InventoryDataModel dataModel;
        private bool flag;

        public MyButton(Context context, string text, int textSize, int imageResId, string color, MyButtonClickListener listener, checkOutViewModel checkOut)
        {
            this.context = context;
            this.text = text;
            this.textSize = textSize;
            this.imageResId = imageResId;
            this.color = color;
            this.listener = listener;
            this.checkOut = checkOut;
            flag = true;

        }
        public MyButton(Context context, string text, int textSize, int imageResId, string color, DeleteButtonClickListener listener, InventoryDataModel inventoryData)
        {
            this.context = context;
            this.text = text;
            this.textSize = textSize;
            this.imageResId = imageResId;
            this.color = color;
            this.deleteListener = listener;
            this.dataModel = inventoryData;
            flag = false;
        }

        public bool OnClick(float x , float y , checkOutViewModel viewModel)
        {
           if (clickRegion != null && clickRegion.Contains(x, y))
              {
                    checkOut = viewModel;
                    listener.OnClick(pos, checkOut);
                    return true;
              }         
            return false;
        }

        public void OnDraw(Canvas c, RectF rectf, int pos)
        {
            Paint p = new Paint();
            p.Color = Color.ParseColor(color);
            c.DrawRect(rectf, p);
            //text
            p.Color = Color.White;
            p.TextSize = textSize;

            Rect r = new Rect();
            float cHeight = rectf.Height();
            float cWidth = rectf.Width();
            p.TextAlign = Paint.Align.Left;
            p.GetTextBounds(text, 0, text.Length, r);
            float x = 0,y = 0;
          
                if (imageResId == 0)//no image
                {
                    x = cWidth / 2f - r.Width() / 2f - r.Left;
                    y = cHeight / 2f + r.Height() / 2f - r.Left;
                    c.DrawText(text, rectf.Left + x, rectf.Top + y, p);
                }
                Drawable d = ContextCompat.GetDrawable(context, imageResId);
                Bitmap bitmap = DrawableToBitmap(d);
            if (flag)
            {
                x = cWidth / 2f - r.Width() / 3f - r.Left;
                y = cHeight / 4f + r.Height() / 2f - r.Left;
                c.DrawBitmap(bitmap, rectf.Left + x, rectf.Top + y, p);
                x = cWidth / 2f - r.Width() / 2f - r.Left;
                y = cHeight / 2f + r.Height() / 2f - r.Left;
                c.DrawText(text, rectf.Left + x, rectf.Top + y, p);
            }

            else
            {
                y = cHeight / 4f + r.Height() / 2f - r.Left;
                x = cWidth / 6.5f - r.Width() / 6.5f - r.Left;
                c.DrawBitmap(bitmap, rectf.Left + x + 10, rectf.Top + y, p);
                
            }  
            clickRegion = rectf;
            this.pos = pos;
        }

        private Bitmap DrawableToBitmap(Drawable d)
        {
            if (d is BitmapDrawable)
                return ((BitmapDrawable)d).Bitmap;
            Bitmap bitmap = Bitmap.CreateBitmap(d.IntrinsicWidth, d.IntrinsicHeight, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(bitmap);
            d.SetBounds(0, 0, canvas.Width, canvas.Height);
            d.Draw(canvas);
            return bitmap;
        }

        public bool OnClick(float x, float y, InventoryDataModel inventoryDataModel)
        {
            if (clickRegion != null && clickRegion.Contains(x, y))
            {
                dataModel = inventoryDataModel;
                deleteListener.OnClick(dataModel);
                return true;
            }

            return false;
        }
    }
}