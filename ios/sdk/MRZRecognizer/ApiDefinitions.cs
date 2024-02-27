using System;
using ObjCRuntime;
using UIKit;
using Foundation;
using CoreGraphics;
using Com.Dynamsoft.Dlr;

namespace MRZRecognizer
{
	// @interface iMRZResult : NSObject
	[BaseType (typeof(NSObject))]
	interface iMRZResult
	{
		// @property (copy, nonatomic) NSString * _Nonnull docId;
		[Export ("docId")]
		string DocId { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull nationality;
		[Export ("nationality")]
		string Nationality { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull issuer;
		[Export ("issuer")]
		string Issuer { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull gender;
		[Export ("gender")]
		string Gender { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull surname;
		[Export ("surname")]
		string Surname { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull givenName;
		[Export ("givenName")]
		string GivenName { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull dateOfBirth;
		[Export ("dateOfBirth")]
		string DateOfBirth { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull dateOfExpiration;
		[Export ("dateOfExpiration")]
		string DateOfExpiration { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull mrzText;
		[Export ("mrzText")]
		string MrzText { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull docType;
		[Export ("docType")]
		string DocType { get; set; }

		// @property (assign, nonatomic) BOOL isParsed;
		[Export ("isParsed")]
		bool IsParsed { get; set; }

		// @property (assign, nonatomic) BOOL isVerified;
		[Export ("isVerified")]
		bool IsVerified { get; set; }
	}

	// @protocol MRZResultListener <NSObject>
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface MRZResultListener
	{
		// @required -(void)mrzResultCallback:(NSInteger)frameId imageData:(id)imageData result:(iMRZResult * _Nullable)result;
		[Abstract]
		[Export ("mrzResultCallback:imageData:result:")]
		void ImageData (nint frameId, NSObject imageData, [NullAllowed] iMRZResult result);
	}

	// @interface DynamsoftMRZRecognizer
	[BaseType (typeof(NSObject))]
	interface DynamsoftMRZRecognizer
	{
		// -(instancetype _Nonnull)initWith:(EnumMRTDDocumentType)type;
		[Export ("initWith:")]
		NativeHandle Constructor (EnumMRTDDocumentType type);

		// -(void)setMRZResultListener:(id<MRZResultListener> _Nullable)listener;
		[Export ("setMRZResultListener:")]
		void SetMRZResultListener ([NullAllowed] MRZResultListener listener);

		// -(iMRZResult * _Nullable)recognizeMRZFromFile:(NSString * _Nonnull)fileName error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("recognizeMRZFromFile(_:)")));
		[Export ("recognizeMRZFromFile:error:")]
		[return: NullAllowed]
		iMRZResult RecognizeMRZFromFile (string fileName, [NullAllowed] out NSError error);

		// -(iMRZResult * _Nullable)recognizeMRZFromBuffer:(id)imageData error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("recognizeMRZFromBuffer(_:)")));
		[Export ("recognizeMRZFromBuffer:error:")]
		[return: NullAllowed]
		iMRZResult RecognizeMRZFromBuffer (NSObject imageData, [NullAllowed] out NSError error);

		// -(iMRZResult * _Nullable)recognizeMRZFromImage:(id)image error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("recognizeMRZFromImage(_:)")));
		[Export ("recognizeMRZFromImage:error:")]
		[return: NullAllowed]
		iMRZResult RecognizeMRZFromImage (NSObject image, [NullAllowed] out NSError error);

		// -(iMRZResult * _Nullable)recognizeMRZFileInMemory:(NSData * _Nonnull)fileBytes error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("recognizeMRZInMemory(_:)")));
		[Export ("recognizeMRZFileInMemory:error:")]
		[return: NullAllowed]
		iMRZResult RecognizeMRZFileInMemory (NSData fileBytes, [NullAllowed] out NSError error);

		// -(NSArray * _Nonnull)recognizeMrzFile:(NSString * _Nonnull)fileName error:(NSError * _Nullable * _Nullable)error;
		[Export ("recognizeMrzFile:error:")]
        iDLRResult[] RecognizeMrzFile (string fileName, [NullAllowed] out NSError error);

		// -(NSArray * _Nonnull)recognizeMrzBuffer:(id)imageData error:(NSError * _Nullable * _Nullable)error;
		[Export ("recognizeMrzBuffer:error:")]
        iDLRResult[] RecognizeMrzBuffer (NSObject imageData, [NullAllowed] out NSError error);

		// -(NSArray * _Nonnull)recognizeMrzImage:(id)image error:(NSError * _Nullable * _Nullable)error;
		[Export ("recognizeMrzImage:error:")]
        iDLRResult[] RecognizeMrzImage (NSObject image, [NullAllowed] out NSError error);
	}
}