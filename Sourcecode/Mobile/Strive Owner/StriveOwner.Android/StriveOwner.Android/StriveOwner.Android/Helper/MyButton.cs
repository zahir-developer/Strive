using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Graphics.Drawables;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.TimInventory;

namespace StriveOwner.Android.Helper
{
    public class MyButton
    {
        
        private int imageResId, textSize, pos;
        private string text, color;
        private RectF clickRegion;
        private MyButtonClickListener listener;
        private DeleteButtonClickListener clickListener;
        private Context context;
        checkOutViewModel checkOut;
        InventoryDataModel dataModel;
        bool flag;
        
        
        public MyButton(Context context, string text,int textSize,int imageResId, string color, MyButtonClickListener listener,checkOutViewModel checkOut)
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
            this.clickListener = listener;
            this.dataModel = inventoryData;
            flag = false;
        }

        public bool OnClick(float x , float y)
        {
            if (flag)
            {
                if (clickRegion != null && clickRegion.Contains(x, y))
                {
                    listener.OnClick(pos, checkOut);
                    return true;
                }
            }
            else 
            {
                if (clickRegion != null && clickRegion.Contains(x, y))
                {
                    clickListener.OnClick(pos, dataModel);
                    return true;
                }
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
            if(imageResId ==0)//no image
            {
                x = cWidth / 2f - r.Width() / 2f - r.Left;
                y= cHeight / 2f + r.Height() / 2f - r.Left;
                c.DrawText(text, rectf.Left + x, rectf.Top + y, p);
            }
            else
            {
                Drawable d = ContextCompat.GetDrawable(context, imageResId);
                Bitmap bitmap = DrawableToBitmap(d);
                x = cWidth / 2f - r.Width() /3f - r.Left;
                y = cHeight / 4f + r.Height() / 2f - r.Left;
                c.DrawBitmap(bitmap, rectf.Left + x, rectf.Top + y, p);
                x = cWidth / 2f - r.Width() / 2f - r.Left;
                y = cHeight / 2f + r.Height() / 2f - r.Left;
                c.DrawText(text, rectf.Left + x, rectf.Top + y, p);
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
    }
}