using UIKit;
using MapKit;
using CoreGraphics;
using Foundation;

namespace Greeter.Modules.Home
{
    public partial class WashTimeViewController: UIViewController, IMKMapViewDelegate
    {
        MKMapView mapView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
        }

        void SetupView()
        {
            mapView = new MKMapView(CGRect.Empty)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                WeakDelegate = this
            };
            View.Add(mapView);

            mapView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            mapView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            mapView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            mapView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
        }

        [Export("mapViewDidFinishRenderingMap:fullyRendered:")]
        public void DidFinishRenderingMap(MKMapView mapView, bool fullyRendered)
        {
            
        }

        [Export("mapViewDidFinishLoadingMap:")]
        public void MapLoaded(MKMapView mapView)
        {

        }
    }
}