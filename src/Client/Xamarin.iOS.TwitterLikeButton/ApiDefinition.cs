using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace TwitterLikeButton.Xamarin.iOS
{
	// The first step to creating a binding is to add your native library ("libNativeLibrary.a")
	// to the project by right-clicking (or Control-clicking) the folder containing this source
	// file and clicking "Add files..." and then simply select the native library (or libraries)
	// that you want to bind.
	//
	// When you do that, you'll notice that MonoDevelop generates a code-behind file for each
	// native library which will contain a [LinkWith] attribute. VisualStudio auto-detects the
	// architectures that the native library supports and fills in that information for you,
	// however, it cannot auto-detect any Frameworks or other system libraries that the
	// native library may depend on, so you'll need to fill in that information yourself.
	//
	// Once you've done that, you're ready to move on to binding the API...
	//
	//
	// Here is where you'd define your API definition for the native Objective-C library.
	//
	// For example, to bind the following Objective-C class:
	//
	//     @interface Widget : NSObject {
	//     }
	//
	// The C# binding would look like this:
	//
	//     [BaseType (typeof (NSObject))]
	//     interface Widget {
	//     }
	//
	// To bind Objective-C properties, such as:
	//
	//     @property (nonatomic, readwrite, assign) CGPoint center;
	//
	// You would add a property definition in the C# interface like so:
	//
	//     [Export ("center")]
	//     CGPoint Center { get; set; }
	//
	// To bind an Objective-C method, such as:
	//
	//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
	//
	// You would add a method definition to the C# interface like so:
	//
	//     [Export ("doSomething:atIndex:")]
	//     void DoSomething (NSObject object, int index);
	//
	// Objective-C "constructors" such as:
	//
	//     -(id)initWithElmo:(ElmoMuppet *)elmo;
	//
	// Can be bound as:
	//
	//     [Export ("initWithElmo:")]
	//     IntPtr Constructor (ElmoMuppet elmo);
	//
	// For more information, see http://docs.xamarin.com/ios/advanced_topics/binding_objective-c_types
	//


	using System;
	using CoreGraphics;
	using Foundation;
	using ObjCRuntime;
	using UIKit;

	// @protocol TTFaveButtonDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface TTFaveButtonDelegate
	{
		// @optional -(BOOL)faveButtonShouldAnimation;
		[Export("faveButtonShouldAnimation")]
		bool FaveButtonShouldAnimation { get; }

		// @optional -(void)faveButton:(TTFaveButton *)button didSelected:(BOOL)selected;
		[Export("faveButton:didSelected:")]
		void FaveButton(TTFaveButton button, bool selected);

		// @optional -(NSArray<NSArray *> *)faveButtonDotColors:(TTFaveButton *)button;
		[Export("faveButtonDotColors:")]
		NSArray[] FaveButtonDotColors(TTFaveButton button);
	}

	// @interface TTFaveButton : UIButton
	[BaseType(typeof(UIButton))]
	interface TTFaveButton
	{
		// @property (nonatomic, strong) UIColor * normalColor;
		[Export("normalColor", ArgumentSemantic.Strong)]
		UIColor NormalColor { get; set; }

		// @property (nonatomic, strong) UIColor * selectedColor;
		[Export("selectedColor", ArgumentSemantic.Strong)]
		UIColor SelectedColor { get; set; }

		// @property (nonatomic, strong) UIColor * dotFirstColor;
		[Export("dotFirstColor", ArgumentSemantic.Strong)]
		UIColor DotFirstColor { get; set; }

		// @property (nonatomic, strong) UIColor * dotSecondColor;
		[Export("dotSecondColor", ArgumentSemantic.Strong)]
		UIColor DotSecondColor { get; set; }

		// @property (nonatomic, strong) UIColor * circleFromColor;
		[Export("circleFromColor", ArgumentSemantic.Strong)]
		UIColor CircleFromColor { get; set; }

		// @property (nonatomic, strong) UIColor * circleToColor;
		[Export("circleToColor", ArgumentSemantic.Strong)]
		UIColor CircleToColor { get; set; }

		[Wrap("WeakDelegate")]
		NSObject Delegate { get; set; }

		// @property (nonatomic, weak) id delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(instancetype)initWithFrame:(CGRect)frame faveIconImage:(UIImage *)faveIconImage selectedFaveIconImage:(UIImage *)selectedFaveIconImage;
		[Export("initWithFrame:faveIconImage:selectedFaveIconImage:")]
		IntPtr Constructor(CGRect frame, UIImage faveIconImage, UIImage selectedFaveIconImage);
	}

	// @interface TTFaveIcon : UIView
	[BaseType(typeof(UIView))]
	interface TTFaveIcon
	{
		// +(TTFaveIcon *)createFaveIcon:(UIView *)onView icon:(UIImage *)icon color:(UIColor *)color;
		[Static]
		[Export("createFaveIcon:icon:color:")]
		TTFaveIcon CreateFaveIcon(UIView onView, UIImage icon, UIColor color);

		// +(TTFaveIcon *)createFaveIcon:(UIView *)onView icon:(UIImage *)icon selectedIcon:(UIImage *)selectedIcon color:(UIColor *)color;
		[Static]
		[Export("createFaveIcon:icon:selectedIcon:color:")]
		TTFaveIcon CreateFaveIcon(UIView onView, UIImage icon, UIImage selectedIcon, UIColor color);

		// -(void)selectWithoutAnimation:(BOOL)isSelected fillColor:(UIColor *)fillColor;
		[Export("selectWithoutAnimation:fillColor:")]
		void SelectWithoutAnimation(bool isSelected, UIColor fillColor);

		// -(void)animateSelect:(BOOL)isSelected fillColor:(UIColor *)fillColor duration:(NSTimeInterval)duration delay:(NSTimeInterval)delay;
		[Export("animateSelect:fillColor:duration:delay:")]
		void AnimateSelect(bool isSelected, UIColor fillColor, double duration, double delay);
	}

	// @interface TTRing : UIView
	[BaseType(typeof(UIView))]
	interface TTRing
	{
		// -(instancetype)initRadius:(CGFloat)radius linwWidth:(CGFloat)lineWidth fillColor:(UIColor *)fillColor;
		[Export("initRadius:linwWidth:fillColor:")]
		IntPtr Constructor(nfloat radius, nfloat lineWidth, UIColor fillColor);

		// -(void)animateToRadius:(CGFloat)radius toColor:(UIColor *)toColor duration:(NSTimeInterval)duration delay:(NSTimeInterval)delay;
		[Export("animateToRadius:toColor:duration:delay:")]
		void AnimateToRadius(nfloat radius, UIColor toColor, double duration, double delay);

		// -(void)animateColapse:(CGFloat)radius duration:(NSTimeInterval)duration delay:(NSTimeInterval)delay;
		[Export("animateColapse:duration:delay:")]
		void AnimateColapse(nfloat radius, double duration, double delay);

		// +(TTRing *)createRingFavaButton:(TTFaveButton *)faveButton radius:(CGFloat)radius lineWidth:(CGFloat)lineWidth fillColor:(UIColor *)fillCorlor;
		[Static]
		[Export("createRingFavaButton:radius:lineWidth:fillColor:")]
		TTRing CreateRingFavaButton(TTFaveButton faveButton, nfloat radius, nfloat lineWidth, UIColor fillCorlor);
	}

	// @interface TTSpark : UIView
	[BaseType(typeof(UIView))]
	interface TTSpark
	{
		// -(void)animateIgniteShow:(CGFloat)radius duration:(NSTimeInterval)duration delay:(NSTimeInterval)delay;
		[Export("animateIgniteShow:duration:delay:")]
		void AnimateIgniteShow(nfloat radius, double duration, double delay);

		// -(void)animateIgniteHide:(NSTimeInterval)duration delay:(NSTimeInterval)delay;
		[Export("animateIgniteHide:delay:")]
		void AnimateIgniteHide(double duration, double delay);

		// +(TTSpark *)createSpark:(TTFaveButton *)faveButton radius:(CGFloat)radius firstColor:(UIColor *)firstColor secondColor:(UIColor *)secondColor angle:(CGFloat)angle dotRadius:(DotRadius)dotRadius;
		[Static]
		[Export("createSpark:radius:firstColor:secondColor:angle:dotRadius:")]
		TTSpark CreateSpark(TTFaveButton faveButton, nfloat radius, UIColor firstColor, UIColor secondColor, nfloat angle, DotRadius dotRadius);
	}

	// @interface TTAutoLayout (UIView)
	[BaseType(typeof(UIView))]
	interface UIView_TTAutoLayout
	{
		// -(NSLayoutConstraint *)TTConstraintForAttribute:(NSLayoutAttribute)attribute;
		[Export("TTConstraintForAttribute:")]
		NSLayoutConstraint TTConstraintForAttribute(NSLayoutAttribute attribute);
	}

	// @interface TTKit (UIView)
	[BaseType(typeof(UIView))]
	interface UIView_TTKit
	{
		// -(UIImage * _Nullable)snapshotImage;
		[NullAllowed, Export("snapshotImage")]
		UIImage SnapshotImage { get; }

		// -(UIImage * _Nullable)snapshotImageAfterScreenUpdates:(BOOL)afterUpdates;
		[Export("snapshotImageAfterScreenUpdates:")]
		[return: NullAllowed]
		UIImage SnapshotImageAfterScreenUpdates(bool afterUpdates);

		// -(NSData * _Nullable)snapshotPDF;
		[NullAllowed, Export("snapshotPDF")]
		NSData SnapshotPDF { get; }

		// -(void)setLayerShadow:(UIColor * _Nullable)color offset:(CGSize)offset radius:(CGFloat)radius;
		[Export("setLayerShadow:offset:radius:")]
		void SetLayerShadow([NullAllowed] UIColor color, CGSize offset, nfloat radius);

		// -(void)removeAllSubviews;
		[Export("removeAllSubviews")]
		void RemoveAllSubviews();

		// @property (readonly, nonatomic) UIViewController * _Nullable viewController;
		[NullAllowed, Export("viewController")]
		UIViewController ViewController { get; }

		// @property (readonly, nonatomic) CGFloat visibleAlpha;
		[Export("visibleAlpha")]
		nfloat VisibleAlpha { get; }

		// -(CGPoint)convertPoint:(CGPoint)point toViewOrWindow:(UIView * _Nullable)view;
		[Export("convertPoint:toViewOrWindow:")]
		CGPoint ConvertPoint(CGPoint point, [NullAllowed] UIView view);
		
		// -(CGRect)convertRect:(CGRect)rect toViewOrWindow:(UIView * _Nullable)view;
		[Export("convertRect:toViewOrWindow:")]
		CGRect ConvertRect(CGRect rect, [NullAllowed] UIView view);
		
		// @property (nonatomic) CGFloat left;
		[Export("left")]
		nfloat Left { get; set; }

		// @property (nonatomic) CGFloat top;
		[Export("top")]
		nfloat Top { get; set; }

		// @property (nonatomic) CGFloat right;
		[Export("right")]
		nfloat Right { get; set; }

		// @property (nonatomic) CGFloat bottom;
		[Export("bottom")]
		nfloat Bottom { get; set; }

		// @property (nonatomic) CGFloat width;
		[Export("width")]
		nfloat Width { get; set; }

		// @property (nonatomic) CGFloat height;
		[Export("height")]
		nfloat Height { get; set; }

		// @property (nonatomic) CGFloat centerX;
		[Export("centerX")]
		nfloat CenterX { get; set; }

		// @property (nonatomic) CGFloat centerY;
		[Export("centerY")]
		nfloat CenterY { get; set; }

		// @property (nonatomic) CGPoint origin;
		[Export("origin", ArgumentSemantic.Assign)]
		CGPoint Origin { get; set; }

		// @property (nonatomic) CGSize size;
		[Export("size", ArgumentSemantic.Assign)]
		CGSize Size { get; set; }
	}

}

