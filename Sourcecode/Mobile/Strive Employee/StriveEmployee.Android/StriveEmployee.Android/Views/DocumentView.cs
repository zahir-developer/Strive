using Android.App;
using Android.OS;
using Android.Widget;

namespace StriveEmployee.Android.Views
{
    [Activity(Label = "DocumentView")]
    public class DocumentView : Activity
    {
        private ImageView Docview;
        private TextView text;

        private string base64 = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DocumentView);
        }
    }
}