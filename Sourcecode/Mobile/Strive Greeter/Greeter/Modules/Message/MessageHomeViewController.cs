using System;
using CoreGraphics;
using Foundation;
using Greeter.Common;
using UIKit;

namespace Greeter.Modules.Message
{
    enum MenuType
    {
        Recent,
        Contact,
        Group
    }

    public partial class MessageHomeViewController : UIViewController, IUIPageViewControllerDelegate, IUIPageViewControllerDataSource
    {
        UIView backgroundContainerView;
        UILabel recentTitleLabel;
        UILabel contactTitleLabel;
        UILabel groupTitleLabel;
        UIView indicatorView;
        UIPageViewController pageViewController;

        readonly UIViewController[] viewControllers = new UIViewController[3];

        NSLayoutConstraint indicatorCenterToRecentTitleConstraint;
        NSLayoutConstraint indicatorCenterToContactTitleConstraint;
        NSLayoutConstraint indicatorCenterToGroupTitleConstraint;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            SetupPageViewController();
            UpdateNaviagtionItem();
        }

        void SetupView()
        {
            var backgroundImage = new UIImageView(UIImage.FromBundle(ImageNames.SPLASH_BG));
            backgroundImage.TranslatesAutoresizingMaskIntoConstraints = false;
            backgroundImage.ContentMode = UIViewContentMode.ScaleAspectFill;
            View.Add(backgroundImage);

            backgroundContainerView = new UIView(CGRect.Empty);
            backgroundContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            backgroundContainerView.BackgroundColor = UIColor.White;
            backgroundContainerView.Layer.CornerRadius = 8;
            backgroundContainerView.Layer.MasksToBounds = true;
            View.Add(backgroundContainerView);

            var horizontalDividerView = new UIView(CGRect.Empty);
            horizontalDividerView.TranslatesAutoresizingMaskIntoConstraints = false;
            horizontalDividerView.BackgroundColor = UIColor.LightGray;
            backgroundContainerView.Add(horizontalDividerView);

            recentTitleLabel = new UILabel(CGRect.Empty);
            recentTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            recentTitleLabel.Text = "Recents";
            recentTitleLabel.Font = UIFont.SystemFontOfSize(16);
            recentTitleLabel.TextColor = UIColor.Black;
            recentTitleLabel.TextAlignment = UITextAlignment.Center;
            recentTitleLabel.UserInteractionEnabled = true;
            recentTitleLabel.AddGestureRecognizer(new UITapGestureRecognizer(RecentTapped));
            backgroundContainerView.Add(recentTitleLabel);

            contactTitleLabel = new UILabel(CGRect.Empty);
            contactTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            contactTitleLabel.Text = "Contacts";
            contactTitleLabel.Font = UIFont.SystemFontOfSize(16);
            contactTitleLabel.TextColor = UIColor.Black;
            contactTitleLabel.TextAlignment = UITextAlignment.Center;
            contactTitleLabel.UserInteractionEnabled = true;
            contactTitleLabel.AddGestureRecognizer(new UITapGestureRecognizer(ContactTapped));
            backgroundContainerView.Add(contactTitleLabel);

            groupTitleLabel = new UILabel(CGRect.Empty);
            groupTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            groupTitleLabel.Text = "Groups";
            groupTitleLabel.Font = UIFont.SystemFontOfSize(16);
            groupTitleLabel.TextColor = UIColor.Black;
            groupTitleLabel.TextAlignment = UITextAlignment.Center;
            groupTitleLabel.UserInteractionEnabled = true;
            groupTitleLabel.AddGestureRecognizer(new UITapGestureRecognizer(GroupTapped));
            backgroundContainerView.Add(groupTitleLabel);

            var moreImageView = new UIImageView(CGRect.Empty);
            moreImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            moreImageView.Image = UIImage.FromBundle(ImageNames.More);
            moreImageView.UserInteractionEnabled = true;
            moreImageView.AddGestureRecognizer(new UITapGestureRecognizer(MoreTapped));
            moreImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            backgroundContainerView.Add(moreImageView);

            indicatorView = new UIView(CGRect.Empty);
            indicatorView.TranslatesAutoresizingMaskIntoConstraints = false;
            indicatorView.BackgroundColor = UIColor.FromRGB(29.0f / 255.0f, 201.0f / 255.0f, 183.0f / 255.0f);
            backgroundContainerView.Add(indicatorView);

            backgroundImage.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            backgroundImage.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            backgroundImage.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            backgroundImage.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            backgroundContainerView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor, constant: 30).Active = true;
            backgroundContainerView.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor, constant: -30).Active = true;
            backgroundContainerView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, constant: 30).Active = true;
            backgroundContainerView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor, constant: -30).Active = true;

            recentTitleLabel.LeadingAnchor.ConstraintEqualTo(backgroundContainerView.LeadingAnchor, constant: 30).Active = true;
            recentTitleLabel.TopAnchor.ConstraintEqualTo(backgroundContainerView.TopAnchor).Active = true;
            recentTitleLabel.WidthAnchor.ConstraintEqualTo(contactTitleLabel.WidthAnchor).Active = true;
            recentTitleLabel.HeightAnchor.ConstraintEqualTo(60).Active = true;
            recentTitleLabel.SetContentCompressionResistancePriority(249, UILayoutConstraintAxis.Horizontal);

            contactTitleLabel.LeadingAnchor.ConstraintEqualTo(recentTitleLabel.TrailingAnchor).Active = true;
            contactTitleLabel.TopAnchor.ConstraintEqualTo(backgroundContainerView.TopAnchor).Active = true;
            contactTitleLabel.WidthAnchor.ConstraintEqualTo(groupTitleLabel.WidthAnchor).Active = true;
            contactTitleLabel.HeightAnchor.ConstraintEqualTo(60).Active = true;
            contactTitleLabel.SetContentCompressionResistancePriority(249, UILayoutConstraintAxis.Horizontal);

            groupTitleLabel.LeadingAnchor.ConstraintEqualTo(contactTitleLabel.TrailingAnchor).Active = true;
            groupTitleLabel.TrailingAnchor.ConstraintEqualTo(moreImageView.LeadingAnchor, constant: -30).Active = true;
            groupTitleLabel.TopAnchor.ConstraintEqualTo(backgroundContainerView.TopAnchor).Active = true;
            groupTitleLabel.HeightAnchor.ConstraintEqualTo(60).Active = true;
            groupTitleLabel.SetContentCompressionResistancePriority(249, UILayoutConstraintAxis.Horizontal);

            moreImageView.TrailingAnchor.ConstraintEqualTo(backgroundContainerView.TrailingAnchor, constant: -30).Active = true;
            moreImageView.TopAnchor.ConstraintEqualTo(backgroundContainerView.TopAnchor, constant: 20).Active = true;
            moreImageView.WidthAnchor.ConstraintEqualTo(20).Active = true;
            moreImageView.HeightAnchor.ConstraintEqualTo(20).Active = true;

            indicatorView.TopAnchor.ConstraintEqualTo(recentTitleLabel.BottomAnchor).Active = true;
            indicatorView.WidthAnchor.ConstraintEqualTo(150).Active = true;
            indicatorView.HeightAnchor.ConstraintEqualTo(3).Active = true;

            indicatorCenterToRecentTitleConstraint = indicatorView.CenterXAnchor.ConstraintEqualTo(recentTitleLabel.CenterXAnchor);
            indicatorCenterToRecentTitleConstraint.Priority = 249;
            indicatorCenterToRecentTitleConstraint.Active = true;

            indicatorCenterToContactTitleConstraint = indicatorView.CenterXAnchor.ConstraintEqualTo(contactTitleLabel.CenterXAnchor);
            indicatorCenterToContactTitleConstraint.Priority = 249;

            indicatorCenterToGroupTitleConstraint = indicatorView.CenterXAnchor.ConstraintEqualTo(groupTitleLabel.CenterXAnchor);
            indicatorCenterToGroupTitleConstraint.Priority = 249;

            horizontalDividerView.LeadingAnchor.ConstraintEqualTo(backgroundContainerView.LeadingAnchor).Active = true;
            horizontalDividerView.TrailingAnchor.ConstraintEqualTo(backgroundContainerView.TrailingAnchor).Active = true;
            horizontalDividerView.TopAnchor.ConstraintEqualTo(recentTitleLabel.BottomAnchor).Active = true;
            horizontalDividerView.HeightAnchor.ConstraintEqualTo(1).Active = true;
        }

        void SetupPageViewController()
        {
            viewControllers[0] = new RecentMessageViewController();
            viewControllers[1] = new ContactViewController();
            viewControllers[2] = new GroupViewController();

            pageViewController = new UIPageViewController(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal)
            {
                WeakDelegate = this,
                WeakDataSource = this
            };
            pageViewController.SetViewControllersAsync(new UIViewController[]
            {
                new RecentMessageViewController()
            }, UIPageViewControllerNavigationDirection.Forward, true);

            pageViewController.SetViewControllers(new[] { viewControllers[0] }, UIPageViewControllerNavigationDirection.Forward, true, null);

            AddChildViewController(pageViewController);
            pageViewController.View.TranslatesAutoresizingMaskIntoConstraints = false;
            backgroundContainerView.Add(pageViewController.View);
            pageViewController.DidMoveToParentViewController(this);

            pageViewController.View.LeadingAnchor.ConstraintEqualTo(backgroundContainerView.LeadingAnchor).Active = true;
            pageViewController.View.TrailingAnchor.ConstraintEqualTo(backgroundContainerView.TrailingAnchor).Active = true;
            pageViewController.View.TopAnchor.ConstraintEqualTo(recentTitleLabel.BottomAnchor, constant: 20).Active = true;
            pageViewController.View.BottomAnchor.ConstraintEqualTo(backgroundContainerView.BottomAnchor).Active = true;
        }

        [Export("pageViewController:didFinishAnimating:previousViewControllers:transitionCompleted:")]
        public void DidFinishAnimating(UIPageViewController pageViewController, bool finished, UIViewController[] previousViewControllers, bool completed)
        {
            var index = Array.IndexOf(viewControllers, pageViewController.ViewControllers[0]);
            if(finished)
            {
                var menuType = index switch
                {
                    0 => MenuType.Recent,
                    1 => MenuType.Contact,
                    2 => MenuType.Group,
                    _ => MenuType.Recent
                };

                UpdateIndicatorPosition(menuType);
            }
        }

        public UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            return referenceViewController switch
            {
                ContactViewController => viewControllers[0],
                GroupViewController => viewControllers[1],
                _ => null
            };
        }

        public UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            return referenceViewController switch
            {
                RecentMessageViewController => viewControllers[1],
                ContactViewController => viewControllers[2],
                _ => null
            };
        }

        void RecentTapped()
        {
            pageViewController.SetViewControllers(new[] { viewControllers[0] }, UIPageViewControllerNavigationDirection.Reverse, true, null);
            UpdateIndicatorPosition(MenuType.Recent);
        }

        void ContactTapped()
        {
            var navigationDirection = pageViewController.ViewControllers[0] is RecentMessageViewController ?
                UIPageViewControllerNavigationDirection.Forward
                : UIPageViewControllerNavigationDirection.Reverse;
            pageViewController.SetViewControllers(new[] { viewControllers[1] }, navigationDirection, true, null);
            UpdateIndicatorPosition(MenuType.Contact);
        }

        void GroupTapped()
        {
            pageViewController.SetViewControllers(new[] { viewControllers[2] }, UIPageViewControllerNavigationDirection.Forward, true, null);
            UpdateIndicatorPosition(MenuType.Group);
        }

        void MoreTapped()
        {

        }

        void UpdateIndicatorPosition(MenuType menuType)
        {
            indicatorCenterToRecentTitleConstraint.Active = false;
            indicatorCenterToContactTitleConstraint.Active = false;
            indicatorCenterToGroupTitleConstraint.Active = false;

            switch (menuType)
            {
                case MenuType.Recent:
                    indicatorCenterToRecentTitleConstraint.Active = true;
                    break;
                case MenuType.Contact:
                    indicatorCenterToContactTitleConstraint.Active = true;
                    break;
                case MenuType.Group:
                    indicatorCenterToGroupTitleConstraint.Active = true;
                    break;
            }

            UIView.AnimateAsync(0.25, () => View.LayoutIfNeeded());
            UpdateNaviagtionItem();
        }

        void UpdateNaviagtionItem()
        {
            var viewController = pageViewController.ViewControllers[0];

            Title = viewController.Title;
            NavigationItem.RightBarButtonItems = viewController.NavigationItem.RightBarButtonItems;
            NavigationItem.LeftBarButtonItems = viewController.NavigationItem.LeftBarButtonItems;
        }
    }
}