using System;
using ObjCRuntime;
using UIKit;
using Foundation;
using CoreGraphics;

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
	interface MRZResultListener
	{
		// @required -(void)mrzResultCallback:(NSInteger)frameId imageData:(id)imageData result:(iMRZResult * _Nullable)result;
		[Abstract]
		[Export ("mrzResultCallback:imageData:result:")]
		void ImageData (nint frameId, NSObject imageData, [NullAllowed] iMRZResult result);
	}

	// @interface DynamsoftMRZRecognizer
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
		NSObject[] RecognizeMrzFile (string fileName, [NullAllowed] out NSError error);

		// -(NSArray * _Nonnull)recognizeMrzBuffer:(id)imageData error:(NSError * _Nullable * _Nullable)error;
		[Export ("recognizeMrzBuffer:error:")]
		NSObject[] RecognizeMrzBuffer (NSObject imageData, [NullAllowed] out NSError error);

		// -(NSArray * _Nonnull)recognizeMrzImage:(id)image error:(NSError * _Nullable * _Nullable)error;
		[Export ("recognizeMrzImage:error:")]
		NSObject[] RecognizeMrzImage (NSObject image, [NullAllowed] out NSError error);
	}
}