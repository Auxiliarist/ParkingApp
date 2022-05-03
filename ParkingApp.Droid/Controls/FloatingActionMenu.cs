using Android.Animation;
using Android.Annotation;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using static Android.Views.GestureDetector;
using static Android.Views.View;

namespace ParkingApp.Droid.Controls
{
    public class FloatingActionMenu : ViewGroup, IOnAttachStateChangeListener
    {
        FloatingActionButton menuButton;
        IList<FloatingActionButton> menuItems;
        IList<View> menuLabels;
        IList<ChildAnimator> menuItemAnimators;
        View menuBackground;

        AnimatorSet openSet = new AnimatorSet();
        AnimatorSet closeSet = new AnimatorSet();
        Animator openOverlay;
        Animator closeOverlay;

        ValueAnimator hideBackgroundAnimator;
        ValueAnimator showBackgroundAnimator;

        IList<IOnMenuItemClickListener> clickListeners;
        IList<IOnMenuToggleListener> openListeners;

        FabDetector fabDetector;

        bool IsOpen;
        bool IsAnimating;
        bool ShowLabels;
        bool CloseOnTouchOutside = true;

        private long actionsDuration;

        Color menuButtonBackground;
        Color menuButtonRipple;
        Color overlayBackground;
        Color labelTextColor;

        int menuButtonSrc;
        int buttonSpacing;
        int maxButtonWidth;
        int labelBackground;
        int labelMarginEnd;
        int menuMarginEnd;
        int menuMarginBottom;
        int overlayDuration;

        float labelTextSize;

        public bool DependsOnBool { get; set; } = false;

        public interface IOnMenuToggleListener
        {
            void OnMenuToggle(bool opened);
        }

        public interface IOnMenuItemClickListener
        {
            void OnMenuItemClick(FloatingActionMenu floatingActionMenu, int index, FloatingActionButton item);
        }

        public FloatingActionMenu(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            TypedArray attributes = context.Theme.ObtainStyledAttributes(attrs, Resource.Styleable.FloatingActionMenu, 0, 0);

            if (attributes != null)
            {
                try
                {
                    overlayBackground = attributes.GetColor(Resource.Styleable.FloatingActionMenu_overlay_color, Color.ParseColor("#7F2A3441"));
                    menuButtonBackground = attributes.GetColor(Resource.Styleable.FloatingActionMenu_base_background, Color.Red);
                    menuButtonRipple = attributes.GetColor(Resource.Styleable.FloatingActionMenu_base_ripple, Color.ParseColor("#66ffffff"));
                    labelTextColor = attributes.GetColor(Resource.Styleable.FloatingActionMenu_label_fontColor, Color.Black);
                    buttonSpacing = attributes.GetDimensionPixelSize(Resource.Styleable.FloatingActionMenu_item_spacing, DpToPx(context, 8f));
                    menuMarginEnd = attributes.GetDimensionPixelSize(Resource.Styleable.FloatingActionMenu_base_marginEnd, 0);
                    menuMarginBottom = attributes.GetDimensionPixelSize(Resource.Styleable.FloatingActionMenu_base_marginBottom, 0);
                    labelMarginEnd = attributes.GetDimensionPixelSize(Resource.Styleable.FloatingActionMenu_label_marginEnd, 0);
                    menuButtonSrc = attributes.GetResourceId(Resource.Styleable.FloatingActionMenu_base_src, Resource.Drawable.ic_add_black_24dp);
                    labelBackground = attributes.GetResourceId(Resource.Styleable.FloatingActionMenu_label_background, Resource.Drawable.label_background);
                    overlayDuration = attributes.GetInteger(Resource.Styleable.FloatingActionMenu_overlay_duration, 500);
                    actionsDuration = attributes.GetInteger(Resource.Styleable.FloatingActionMenu_actions_duration, 300);
                    labelTextSize = attributes.GetFloat(Resource.Styleable.FloatingActionMenu_label_fontSize, 12f);
                    ShowLabels = attributes.GetBoolean(Resource.Styleable.FloatingActionMenu_enable_labels, true);
                }
                finally
                {
                    attributes.Recycle();
                }

                menuItems = new List<FloatingActionButton>();
                menuLabels = new List<View>();
                menuItemAnimators = new List<ChildAnimator>();
                clickListeners = new List<IOnMenuItemClickListener>();
                openListeners = new List<IOnMenuToggleListener>();

                fabDetector = new FabDetector(Context, new FabDetectorListener(this));

                menuButton = new FloatingActionButton(Context);
                menuButton.Size = FloatingActionButton.SizeAuto;
                menuButton.BackgroundTintList = ColorStateList.ValueOf(menuButtonBackground);
                menuButton.RippleColor = menuButtonRipple;
                menuButton.SetImageResource(menuButtonSrc);
                menuButton.Click += MenuButton_Click;

                menuBackground = new View(Context);
                menuBackground.SetBackgroundColor(overlayBackground);
                menuBackground.AddOnAttachStateChangeListener(this);

                AddViewInLayout(menuButton, -1, new LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent));
                AddView(menuBackground);
            }
        }

        public FloatingActionMenu(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Toggle();
        }

        public void OnViewAttachedToWindow(View attachedView)
        {
            CreateDefaultIconAnimation();
        }

        public void OnViewDetachedFromWindow(View detachedView)
        {

        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            if (changed)
            {
                menuBackground.Layout(l, 0, r, b);

                int buttonsHorizontalCenter = r - l - menuButton.MeasuredWidth / 2 - PaddingRight - menuMarginEnd;
                int menuButtonTop = b - t - menuButton.MeasuredHeight - PaddingBottom - menuMarginBottom;
                int menuButtonLeft = buttonsHorizontalCenter - menuButton.MeasuredWidth / 2;

                menuButton.Layout(menuButtonLeft, menuButtonTop, menuButtonLeft + menuButton.MeasuredWidth, menuButtonTop + menuButton.MeasuredHeight);

                int nextY = menuButtonTop;
                int itemCount = menuItems.Count;

                for (int i = 0; i < itemCount; i++)
                {
                    var item = menuItems.ElementAt(i);

                    if (item.Visibility != ViewStates.Gone)
                    {
                        int childX = buttonsHorizontalCenter - item.MeasuredWidth / 2;
                        int childY = nextY - item.MeasuredHeight - buttonSpacing;

                        item.Layout(childX, childY, childX + item.MeasuredWidth, childY + item.MeasuredHeight);

                        var label = (View)item.Tag;
                        if (label != null)
                        {
                            int labelsOffset = item.MeasuredWidth / 2 + labelMarginEnd;
                            int labelXEnd = buttonsHorizontalCenter - labelsOffset;
                            int labelXStart = labelXEnd - label.MeasuredWidth;
                            int labelTop = childY + (item.MeasuredHeight - label.MeasuredHeight) / 2;

                            label.Layout(labelXStart, labelTop, labelXEnd, labelTop + label.MeasuredHeight);

                            if (!IsAnimating)
                                if (IsOpen)
                                    label.Visibility = ViewStates.Invisible;
                        }

                        nextY = childY;

                        if (!IsAnimating)
                            if (!IsOpen)
                            {
                                item.TranslationY = menuButton.Top - item.Top;
                                item.Visibility = ViewStates.Invisible;
                                menuBackground.Visibility = ViewStates.Invisible;
                            }
                    }
                }

                if (!IsAnimating && Background != null)
                {
                    if (IsOpen)
                        Background.Alpha = 0;
                    else
                        Background.Alpha = 0xff;
                }
            }
        }

        protected override void OnFinishInflate()
        {
            BringChildToFront(menuButton);
            base.OnFinishInflate();
        }

        public override void AddView(View child, int index, LayoutParams @params)
        {
            base.AddView(child, index, @params);
            if (child is FloatingActionButton fab)
            {
                fab.LayoutParameters = @params;
                AddMenuItem(fab);
            }
        }

        public void AddMenuItem(FloatingActionButton item)
        {
            menuItems.Add(item);

            if (ShowLabels)
                BuildLabelButton(item);

            menuItemAnimators.Add(new ChildAnimator(this, item));
            item.SetOnClickListener(new OnItemClickListener(this));
        }

        private void BuildLabelButton(FloatingActionButton item)
        {
            View label = LayoutInflater.From(Context).Inflate(Resource.Layout.fab_label, null);
            label.SetBackgroundResource(labelBackground);

            TextView textView = label.FindViewById<TextView>(Resource.Id.fab_label_text);
            textView.Text = item.ContentDescription;
            textView.SetTextColor(labelTextColor);
            textView.TextSize = labelTextSize;

            label.Visibility = ViewStates.Gone;

            AddView(label);

            menuLabels.Add(label);
            item.Tag = label;

            label.SetOnClickListener(new OnItemClickListener(this));
        }

        public void Toggle()
        {
            //if (!IsOpen && !DependsOnBool)
            //    TriggerClickListeners(-1, menuButton);
            //else if (!IsOpen)
            //    Open();
            //else
            //    Close();

            if (!IsOpen)
                Open();
            else
                Close();
        }

        public void Open()
        {
            StartOpenAnimator();
            IsOpen = true;

            TriggerOpenListeners(true);
        }

        public void Close()
        {
            StartCloseAnimator();
            IsOpen = false;

            TriggerOpenListeners(false);
        }

        private void TriggerOpenListeners(bool state)
        {
            if (openListeners.Count > 0)
            {
                foreach (var listener in openListeners)
                    listener.OnMenuToggle(state);
            }
        }

        private void TriggerClickListeners(int index, FloatingActionButton item)
        {
            if (clickListeners.Count > 0)
            {
                foreach (var listener in clickListeners)
                    listener.OnMenuItemClick(this, index, item);
            }
        }

        // Edit
        [TargetApi(Value = (int)BuildVersionCodes.Lollipop)]
        private void CreateOverlayRipple()
        {
            var manager = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            var display = manager.DefaultDisplay;
            var size = new Point();
            display.GetSize(size);

            int radius = menuButton.Height / 2;

            openOverlay = ObjectAnimator.OfFloat(menuBackground, "Alpha", 0f, 1f);
            openOverlay.SetDuration(overlayDuration);
            openOverlay.SetInterpolator(new AccelerateDecelerateInterpolator());
            openOverlay.AnimationStart += OpenOverlay_AnimationStart;

            closeOverlay = ObjectAnimator.OfFloat(menuBackground, "Alpha", 1f, 0f);
            closeOverlay.SetDuration(overlayDuration);
            closeOverlay.SetInterpolator(new AccelerateDecelerateInterpolator());
            closeOverlay.AnimationEnd += CloseOverlay_AnimationEnd;
        }

        private void OpenOverlay_AnimationStart(object sender, EventArgs e)
        {
            openOverlay.AnimationStart -= OpenOverlay_AnimationStart;
            menuBackground.Visibility = ViewStates.Visible;
        }

        private void CloseOverlay_AnimationEnd(object sender, EventArgs e)
        {
            closeOverlay.AnimationEnd -= CloseOverlay_AnimationEnd;
            menuBackground.Visibility = ViewStates.Gone;
        }

        protected void StartCloseAnimator()
        {
            closeSet.Start();
            closeOverlay.Start();
            foreach (var anim in menuItemAnimators)
                anim.StartCloseAnimator();
        }

        protected void StartOpenAnimator()
        {
            CreateOverlayRipple();
            openOverlay.Start();
            openSet.Start();
            foreach (var anim in menuItemAnimators)
                anim.StartOpenAnimator();
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int widthSize = MeasureSpec.GetSize(widthMeasureSpec);
            MeasureSpecMode widthMode = MeasureSpec.GetMode(widthMeasureSpec);
            int width;
            int heightSize = MeasureSpec.GetSize(heightMeasureSpec);
            MeasureSpecMode heightMode = MeasureSpec.GetMode(heightMeasureSpec);
            int height;

            int count = ChildCount;
            maxButtonWidth = 0;

            for (int i = 0; i < count; i++)
            {
                var child = GetChildAt(i);
                MeasureChild(child, widthMeasureSpec, heightMeasureSpec);
            }

            for (int i = 0; i < menuItems.Count; i++)
            {
                var fab = menuItems.ElementAt(i);
                if (menuLabels.Count > 0)
                {
                    var label = menuLabels.ElementAt(i);
                    maxButtonWidth = Math.Max(maxButtonWidth, label.MeasuredWidth + fab.MeasuredWidth + fab.PaddingEnd + fab.PaddingStart);
                }
            }

            maxButtonWidth = Math.Max(menuButton.MeasuredWidth, maxButtonWidth);

            if (widthMode == MeasureSpecMode.Exactly)
                width = widthSize;
            else
                width = maxButtonWidth;

            if (heightMode == MeasureSpecMode.Exactly)
                height = heightSize;
            else
            {
                int heightSum = 0;
                for (int i = 0; i < count; i++)
                {
                    var child = GetChildAt(i);
                    heightSum += child.MeasuredHeight + child.PaddingBottom;
                }
                height = heightSum;
            }

            SetMeasuredDimension(ResolveSize(width, widthMeasureSpec), ResolveSize(height, heightMeasureSpec));

        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (CloseOnTouchOutside)
                return fabDetector.OnTouchEvent(e);
            else
                return base.OnTouchEvent(e);
        }

        private void CreateDefaultIconAnimation()
        {
            ObjectAnimator collapseAnimator = ObjectAnimator.OfFloat(menuButton, "Rotation", 135f, 0f);
            ObjectAnimator expandAnimator = ObjectAnimator.OfFloat(menuButton, "Rotation", 0f, 135f);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                openSet.PlayTogether(expandAnimator);
                closeSet.PlayTogether(collapseAnimator);
            }
            else
            {
                hideBackgroundAnimator = ValueAnimator.OfInt(0xff, 0);
                hideBackgroundAnimator.Update += HideBackgroundAnimator_Update;

                showBackgroundAnimator = ValueAnimator.OfInt(0, 0xff);
                showBackgroundAnimator.Update += ShowBackgroundAnimator_Update;

                openSet.PlayTogether(expandAnimator, showBackgroundAnimator);
                closeSet.PlayTogether(collapseAnimator, hideBackgroundAnimator);
            }

            openSet.SetInterpolator(new OvershootInterpolator());
            closeSet.SetInterpolator(new AnticipateInterpolator());

            openSet.SetDuration(actionsDuration);
            closeSet.SetDuration(actionsDuration);

            openSet.AddListener(new DefaultIconAnimationListener(this));
            closeSet.AddListener(new DefaultIconAnimationListener(this));
        }

        private void ShowBackgroundAnimator_Update(object sender, ValueAnimator.AnimatorUpdateEventArgs e)
        {
            var alpha = (int)e.Animation.AnimatedValue;
            Background.Alpha = (alpha > 0xff ? 0xff : alpha);
        }

        private void HideBackgroundAnimator_Update(object sender, ValueAnimator.AnimatorUpdateEventArgs e)
        {
            var alpha = (int)e.Animation.AnimatedValue;
            Background.Alpha = (alpha > 0xff ? 0xff : alpha);
        }

        static int DpToPx(Context context, float dp)
        {
            float scale = context.Resources.DisplayMetrics.Density;
            return (int)Math.Round(dp * scale);
        }

        public void AddClickListener(IOnMenuItemClickListener listener)
        {
            clickListeners.Add(listener);
        }

        public void AddToggleListener(IOnMenuToggleListener listener)
        {
            openListeners.Add(listener);
        }

        public bool RemoveClickListener(IOnMenuItemClickListener listener)
        {
            return clickListeners.Remove(listener);
        }

        public bool RemoveToggleListener(IOnMenuToggleListener listener)
        {
            return openListeners.Remove(listener);
        }

        private class FabDetector : GestureDetector
        {
            public FabDetector(IOnGestureListener listener) : base(listener)
            {
            }

            public FabDetector(Context context, IOnGestureListener listener) : base(context, listener)
            {

            }

            public FabDetector(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
            {
            }

            public FabDetector(IOnGestureListener listener, Handler handler) : base(listener, handler)
            {
            }

            public FabDetector(Context context, IOnGestureListener listener, Handler handler) : base(context, listener, handler)
            {
            }

            public FabDetector(Context context, IOnGestureListener listener, Handler handler, bool unused) : base(context, listener, handler, unused)
            {
            }
        }

        private class FabDetectorListener : SimpleOnGestureListener
        {
            FloatingActionMenu menu;

            public FabDetectorListener(FloatingActionMenu menu)
            {
                this.menu = menu;
            }

            public FabDetectorListener(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
            {
            }

            public override bool OnDown(MotionEvent e)
            {
                return menu.CloseOnTouchOutside && menu.IsOpen;
            }

            public override bool OnSingleTapUp(MotionEvent e)
            {
                menu.Close();
                return true;
            }
        }

        private class OnItemClickListener : Java.Lang.Object, IOnClickListener, Animator.IAnimatorListener
        {
            FloatingActionMenu menu;
            View v;

            public OnItemClickListener(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
            {
            }

            public OnItemClickListener(FloatingActionMenu menu)
            {
                this.menu = menu;
            }

            public void OnClick(View v)
            {
                this.v = v;

                menu.closeSet.AddListener(this);

                menu.Close();
            }

            public void OnAnimationCancel(Animator animation)
            {
            }

            public void OnAnimationEnd(Animator animation)
            {
                menu.closeSet.RemoveListener(this);

                if (v is FloatingActionButton fab)
                {
                    int i = menu.menuItems.IndexOf(fab);
                    menu.TriggerClickListeners(i, fab);
                }
                else if (v != menu.menuBackground)
                {
                    int i = menu.menuLabels.IndexOf(v);
                    menu.TriggerClickListeners(i, menu.menuItems.ElementAt(i));
                }

                this.v = null;
            }

            public void OnAnimationRepeat(Animator animation)
            {
            }

            public void OnAnimationStart(Animator animation)
            {
            }
        }

        private class ChildAnimator : Java.Lang.Object, Animator.IAnimatorListener
        {
            FloatingActionMenu menu;
            private View view, label;
            private AlphaAnimation openAnimation, closeAnimation;
            private bool playingOpenAnimator;

            public ChildAnimator(FloatingActionMenu menu, View view)
            {
                this.menu = menu;

                view.Animate().SetListener(this);

                if (menu.ShowLabels)
                {
                    label = (View)view.Tag;
                    openAnimation = new AlphaAnimation(0f, 1f)
                    {
                        Duration = 250
                    };

                    closeAnimation = new AlphaAnimation(1f, 0f)
                    {
                        Duration = 250
                    };
                }

                this.view = view;
            }

            public void StartOpenAnimator()
            {
                view.Animate().Cancel();
                playingOpenAnimator = true;

                view.Animate()
                    .TranslationY(0f)
                    .SetInterpolator(new OvershootInterpolator())
                    .Start();
            }

            public void StartCloseAnimator()
            {
                view.Animate().Cancel();
                playingOpenAnimator = false;

                if (menu.ShowLabels)
                    label.StartAnimation(closeAnimation);

                view.Animate()
                    .TranslationY(menu.menuButton.Top - view.Top)
                    .SetInterpolator(new AnticipateInterpolator())
                    .Start();
            }

            public void OnAnimationStart(Animator animation)
            {
                if (playingOpenAnimator)
                    view.Visibility = ViewStates.Visible;
                else
                {
                    if (menu.ShowLabels)
                    {
                        label.Visibility = ViewStates.Gone;
                    }
                }
            }

            public void OnAnimationEnd(Animator animation)
            {
                if (!playingOpenAnimator)
                    view.Visibility = ViewStates.Gone;
                else
                {
                    if (menu.ShowLabels)
                    {
                        label.Visibility = ViewStates.Visible;
                        label.StartAnimation(openAnimation);
                    }
                }
            }

            public void OnAnimationCancel(Animator animation)
            {
            }

            public void OnAnimationRepeat(Animator animation)
            {
            }

        }

        private class DefaultIconAnimationListener : Java.Lang.Object, Animator.IAnimatorListener
        {
            FloatingActionMenu menu;

            public DefaultIconAnimationListener(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
            {
            }

            public DefaultIconAnimationListener(FloatingActionMenu menu)
            {
                this.menu = menu;
            }

            public void OnAnimationStart(Animator animation)
            {
                menu.IsAnimating = true;
            }

            public void OnAnimationEnd(Animator animation)
            {
                menu.IsAnimating = false;
            }

            public void OnAnimationCancel(Animator animation)
            {
                menu.IsAnimating = false;
            }

            public void OnAnimationRepeat(Animator animation)
            {
            }
        }

        protected override IParcelable OnSaveInstanceState()
        {
            Bundle bundle = new Bundle();
            bundle.PutParcelable("instanceState", base.OnSaveInstanceState());
            bundle.PutBoolean("openState", IsOpen);
            return bundle;
        }

        protected override void OnRestoreInstanceState(IParcelable state)
        {
            if (state is Bundle bundle)
            {
                IsOpen = bundle.GetBoolean("openState");
                state = bundle.GetParcelable("instanceState").JavaCast<IParcelable>();
            }

            base.OnRestoreInstanceState(state);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                menuButton.Click -= MenuButton_Click;
                hideBackgroundAnimator.Update -= HideBackgroundAnimator_Update;
                showBackgroundAnimator.Update -= ShowBackgroundAnimator_Update;
            }

            base.Dispose(disposing);
        }

    }
}