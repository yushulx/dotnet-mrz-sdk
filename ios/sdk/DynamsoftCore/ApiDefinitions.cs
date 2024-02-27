using System;
using ObjCRuntime;
using UIKit;
using Foundation;
using CoreGraphics;

namespace DynamsoftCore
{

	// @interface iQuadrilateral : NSObject
	[BaseType (typeof(NSObject))]
	interface iQuadrilateral
	{
		// @property (nonatomic) NSArray * _Nonnull points;
		[Export ("points", ArgumentSemantic.Assign)]
		NSObject[] Points { get; set; }

		// -(BOOL)isPointInQuadrilateral:(CGPoint)point __attribute__((swift_name("isPointInQuadrilateral(_:)")));
		[Export ("isPointInQuadrilateral:")]
		bool IsPointInQuadrilateral (CGPoint point);

		// -(CGRect)getBoundingRect;
		[Export ("getBoundingRect")]
		CGRect BoundingRect { get; }
	}

	// @interface iRegionDefinition : NSObject
	[BaseType (typeof(NSObject))]
	interface iRegionDefinition
	{
		// @property (assign, nonatomic) NSInteger regionTop;
		[Export ("regionTop")]
		nint RegionTop { get; set; }

		// @property (assign, nonatomic) NSInteger regionLeft;
		[Export ("regionLeft")]
		nint RegionLeft { get; set; }

		// @property (assign, nonatomic) NSInteger regionRight;
		[Export ("regionRight")]
		nint RegionRight { get; set; }

		// @property (assign, nonatomic) NSInteger regionBottom;
		[Export ("regionBottom")]
		nint RegionBottom { get; set; }

		// @property (assign, nonatomic) NSInteger regionMeasuredByPercentage;
		[Export ("regionMeasuredByPercentage")]
		nint RegionMeasuredByPercentage { get; set; }
	}

	// @interface iImageData : NSObject
	[BaseType (typeof(NSObject))]
	interface iImageData
	{
		// @property (nonatomic) NSData * _Nullable bytes;
		[NullAllowed, Export ("bytes", ArgumentSemantic.Assign)]
		NSData Bytes { get; set; }

		// @property (assign, nonatomic) NSInteger width;
		[Export ("width")]
		nint Width { get; set; }

		// @property (assign, nonatomic) NSInteger height;
		[Export ("height")]
		nint Height { get; set; }

		// @property (assign, nonatomic) NSInteger stride;
		[Export ("stride")]
		nint Stride { get; set; }

		// @property (assign, nonatomic) EnumImagePixelFormat format;
		[Export ("format", ArgumentSemantic.Assign)]
		EnumImagePixelFormat Format { get; set; }

		// @property (assign, nonatomic) NSInteger orientation;
		[Export ("orientation")]
		nint Orientation { get; set; }

		// -(UIImage * _Nullable)toUIImage:(NSError * _Nullable * _Nullable)error;
		[Export ("toUIImage:")]
		[return: NullAllowed]
		UIImage ToUIImage ([NullAllowed] out NSError error);
	}

	// @interface iBarcodeResult : NSObject
	[BaseType (typeof(NSObject))]
	interface iBarcodeResult
	{
		// @property (assign, nonatomic) EnumBarcodeFormat barcodeFormat;
		[Export ("barcodeFormat", ArgumentSemantic.Assign)]
		EnumBarcodeFormat BarcodeFormat { get; set; }

		// @property (assign, nonatomic) EnumBarcodeFormat2 barcodeFormat_2;
		[Export ("barcodeFormat_2", ArgumentSemantic.Assign)]
		EnumBarcodeFormat2 BarcodeFormat_2 { get; set; }

		// @property (nonatomic) NSString * _Nullable text;
		[NullAllowed, Export ("text")]
		string Text { get; set; }

		// @property (nonatomic) NSData * _Nullable bytes;
		[NullAllowed, Export ("bytes", ArgumentSemantic.Assign)]
		NSData Bytes { get; set; }

		// @property (nonatomic) iQuadrilateral * _Nullable location;
		[NullAllowed, Export ("location", ArgumentSemantic.Assign)]
		iQuadrilateral Location { get; set; }

		// @property (assign, nonatomic) NSInteger moduleSize;
		[Export ("moduleSize")]
		nint ModuleSize { get; set; }

		// @property (assign, nonatomic) NSInteger pageNumber;
		[Export ("pageNumber")]
		nint PageNumber { get; set; }
	}

	// @interface DynamsoftLicenseManager : NSObject
	[BaseType (typeof(NSObject))]
	interface DynamsoftLicenseManager
	{
		// +(void)initLicense:(NSString * _Nonnull)license verificationDelegate:(id _Nullable)connectionDelegate;
		[Static]
		[Export ("initLicense:verificationDelegate:")]
		void InitLicense (string license, [NullAllowed] NSObject connectionDelegate);
	}

	// @protocol LicenseVerificationListener <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface LicenseVerificationListener
	{
		// @required -(void)licenseVerificationCallback:(_Bool)isSuccess error:(NSError * _Nullable)error;
		[Abstract]
		[Export ("licenseVerificationCallback:error:")]
		void LicenseVerificationCallback (bool isSuccess, [NullAllowed] NSError error);
	}

	// @protocol ImageSource <NSObject>
	/*
	Check whether adding [Model] to this declaration is appropriate.
	[Model] is used to generate a C# class that implements this protocol,
	and might be useful for protocols that consumers are supposed to implement,
	since consumers can subclass the generated class instead of implementing
	the generated interface. If consumers are not supposed to implement this
	protocol, then [Model] is redundant and will generate code that will never
	be used.
	*/[Protocol]
	[BaseType (typeof(NSObject))]
	interface ImageSource
	{
		// @required -(iImageData * _Nullable)getImage;
		[Abstract]
		[NullAllowed, Export ("getImage")]
		iImageData Image { get; }
	}
}