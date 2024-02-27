using System;
using ObjCRuntime;
using UIKit;
using Foundation;
using CoreGraphics;
using DynamsoftCore;

namespace Com.Dynamsoft.Dlr
{

	// @interface iDLRReferenceRegion : NSObject
	[BaseType(typeof(NSObject))]
	interface iDLRReferenceRegion
	{
		// @property (assign, nonatomic) EnumLocalizationSourceType localizationSourceType;
		[Export("localizationSourceType", ArgumentSemantic.Assign)]
		EnumLocalizationSourceType LocalizationSourceType { get; set; }

		// @property (nonatomic) int * _Nullable location;
		[NullAllowed, Export("location", ArgumentSemantic.Assign)]
		iQuadrilateral Location { get; set; }

		// @property (assign, nonatomic) NSInteger regionMeasuredByPercentage;
		[Export("regionMeasuredByPercentage")]
		nint RegionMeasuredByPercentage { get; set; }

		// @property (assign, nonatomic) NSInteger regionPredetectionModesIndex;
		[Export("regionPredetectionModesIndex")]
		nint RegionPredetectionModesIndex { get; set; }

		// @property (assign, nonatomic) NSInteger barcodeFormatIds;
		[Export("barcodeFormatIds")]
		nint BarcodeFormatIds { get; set; }

		// @property (assign, nonatomic) NSInteger barcodeFormatIds_2;
		[Export("barcodeFormatIds_2")]
		nint BarcodeFormatIds_2 { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable barcodeTextRegExPattern;
		[NullAllowed, Export("barcodeTextRegExPattern")]
		string BarcodeTextRegExPattern { get; set; }
	}

	// @interface iDLRDictionaryCorrectionThreshold : NSObject
	[BaseType(typeof(NSObject))]
	interface iDLRDictionaryCorrectionThreshold
	{
		// @property (assign, nonatomic) NSInteger minWordLength;
		[Export("minWordLength")]
		nint MinWordLength { get; set; }

		// @property (assign, nonatomic) NSInteger maxWordLength;
		[Export("maxWordLength")]
		nint MaxWordLength { get; set; }

		// @property (assign, nonatomic) NSInteger threshold;
		[Export("threshold")]
		nint Threshold { get; set; }
	}

	// @interface iDLRFurtherModes : NSObject
	[BaseType(typeof(NSObject))]
	interface iDLRFurtherModes
	{
		// @property (readwrite, nonatomic) NSArray * _Nullable colourConversionModes;
		[NullAllowed, Export("colourConversionModes", ArgumentSemantic.Assign)]
		NSObject[] ColourConversionModes { get; set; }

		// @property (readwrite, nonatomic) NSArray * _Nullable grayscaleTransformationModes;
		[NullAllowed, Export("grayscaleTransformationModes", ArgumentSemantic.Assign)]
		NSObject[] GrayscaleTransformationModes { get; set; }

		// @property (readwrite, nonatomic) NSArray * _Nullable regionPredetectionModes;
		[NullAllowed, Export("regionPredetectionModes", ArgumentSemantic.Assign)]
		NSObject[] RegionPredetectionModes { get; set; }

		// @property (readwrite, nonatomic) NSArray * _Nullable grayscaleEnhancementModes;
		[NullAllowed, Export("grayscaleEnhancementModes", ArgumentSemantic.Assign)]
		NSObject[] GrayscaleEnhancementModes { get; set; }

		// @property (readwrite, nonatomic) NSArray * _Nullable textureDetectionModes;
		[NullAllowed, Export("textureDetectionModes", ArgumentSemantic.Assign)]
		NSObject[] TextureDetectionModes { get; set; }
	}

	// @interface iDLRRuntimeSettings : NSObject
	[BaseType(typeof(NSObject))]
	interface iDLRRuntimeSettings
	{
		// @property (assign, nonatomic) NSInteger maxThreadCount;
		[Export("maxThreadCount")]
		nint MaxThreadCount { get; set; }

		// @property (assign, nonatomic) NSInteger timeout;
		[Export("timeout")]
		nint Timeout { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable characterModelName;
		[NullAllowed, Export("characterModelName")]
		string CharacterModelName { get; set; }

		// @property (nonatomic) iDLRReferenceRegion * _Nullable referenceRegion;
		[NullAllowed, Export("referenceRegion", ArgumentSemantic.Assign)]
		iDLRReferenceRegion ReferenceRegion { get; set; }

		// @property (nonatomic) int * _Nullable textArea;
		//[NullAllowed, Export("textArea", ArgumentSemantic.Assign)]
		//unsafe int* TextArea { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable dictionaryPath;
		[NullAllowed, Export("dictionaryPath")]
		string DictionaryPath { get; set; }

		// @property (nonatomic) iDLRDictionaryCorrectionThreshold * _Nullable dictionaryCorrectionThreshold;
		[NullAllowed, Export("dictionaryCorrectionThreshold", ArgumentSemantic.Assign)]
		iDLRDictionaryCorrectionThreshold DictionaryCorrectionThreshold { get; set; }

		// @property (readwrite, nonatomic) NSArray * _Nullable binarizationModes;
		[NullAllowed, Export("binarizationModes", ArgumentSemantic.Assign)]
		NSObject[] BinarizationModes { get; set; }

		// @property (nonatomic) iDLRFurtherModes * _Nullable furtherModes;
		[NullAllowed, Export("furtherModes", ArgumentSemantic.Assign)]
		iDLRFurtherModes FurtherModes { get; set; }
	}

	// @interface iDLRCharacterResult : NSObject
	[BaseType(typeof(NSObject))]
	interface iDLRCharacterResult
	{
		// @property (copy, nonatomic) NSString * _Nullable characterH;
		[NullAllowed, Export("characterH")]
		string CharacterH { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable characterM;
		[NullAllowed, Export("characterM")]
		string CharacterM { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable characterL;
		[NullAllowed, Export("characterL")]
		string CharacterL { get; set; }

		// @property (nonatomic) int * _Nullable location;
		[NullAllowed, Export("location", ArgumentSemantic.Assign)]
		iQuadrilateral Location { get; set; }

		// @property (assign, nonatomic) NSInteger characterHConfidence;
		[Export("characterHConfidence")]
		nint CharacterHConfidence { get; set; }

		// @property (assign, nonatomic) NSInteger characterMConfidence;
		[Export("characterMConfidence")]
		nint CharacterMConfidence { get; set; }

		// @property (assign, nonatomic) NSInteger characterLConfidence;
		[Export("characterLConfidence")]
		nint CharacterLConfidence { get; set; }
	}

	// @interface iDLRLineResult : NSObject
	[BaseType(typeof(NSObject))]
	interface iDLRLineResult
	{
		// @property (copy, nonatomic) NSString * _Nullable lineSpecificationName;
		[NullAllowed, Export("lineSpecificationName")]
		string LineSpecificationName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable text;
		[NullAllowed, Export("text")]
		string Text { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable characterModelName;
		[NullAllowed, Export("characterModelName")]
		string CharacterModelName { get; set; }

		// @property (nonatomic) int * _Nullable location;
		[NullAllowed, Export("location", ArgumentSemantic.Assign)]
		iQuadrilateral Location { get; set; }

		// @property (assign, nonatomic) NSInteger confidence;
		[Export("confidence")]
		nint Confidence { get; set; }

		// @property (nonatomic) NSArray<iDLRCharacterResult *> * _Nullable characterResults;
		[NullAllowed, Export("characterResults", ArgumentSemantic.Assign)]
		iDLRCharacterResult[] CharacterResults { get; set; }
	}

	// @interface iDLRResult : NSObject
	[BaseType(typeof(NSObject))]
	interface iDLRResult
	{
		// @property (copy, nonatomic) NSString * _Nullable referenceRegionName;
		[NullAllowed, Export("referenceRegionName")]
		string ReferenceRegionName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable textAreaName;
		[NullAllowed, Export("textAreaName")]
		string TextAreaName { get; set; }

		// @property (nonatomic) int * _Nullable location;
		[NullAllowed, Export("location", ArgumentSemantic.Assign)]
		iQuadrilateral Location { get; set; }

		// @property (assign, nonatomic) NSInteger confidence;
		[Export("confidence")]
		nint Confidence { get; set; }

		// @property (nonatomic) NSArray<iDLRLineResult *> * _Nullable lineResults;
		[NullAllowed, Export("lineResults", ArgumentSemantic.Assign)]
		iDLRLineResult[] LineResults { get; set; }

		// @property (assign, nonatomic) NSInteger pageNumber;
		[Export("pageNumber")]
		nint PageNumber { get; set; }
	}

	// @protocol LabelResultListener <NSObject>
	/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/
	// [Protocol]
	// [BaseType(typeof(NSObject))]
	// interface LabelResultListener
	// {
	// 	// @required -(void)labelResultCallback:(NSInteger)frameId imageData:(id)imageData results:(NSArray<iDLRResult *> * _Nullable)results;
	// 	[Abstract]
	// 	[Export("labelResultCallback:imageData:results:")]
	// 	void ImageData(nint frameId, NSObject imageData, [NullAllowed] iDLRResult[] results);
	// }

	// @interface DynamsoftLabelRecognizer : NSObject
	[BaseType(typeof(NSObject))]
	interface DynamsoftLabelRecognizer
	{
		// +(NSString * _Nonnull)getVersion;
		[Static]
		[Export("getVersion")]
		string Version { get; }

		// -(void)setLabelResultListener:(id<LabelResultListener> _Nullable)listener;
		// [Export("setLabelResultListener:")]
		// void SetLabelResultListener([NullAllowed] LabelResultListener listener);

		// -(void)setImageSource:(id _Nonnull)source;
		[Export("setImageSource:")]
		void SetImageSource(NSObject source);

		// -(void)startScanning;
		[Export("startScanning")]
		void StartScanning();

		// -(void)stopScanning;
		[Export("stopScanning")]
		void StopScanning();

		// -(BOOL)initRuntimeSettings:(NSString * _Nonnull)content error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("initRuntimeSettings(_:)")));
		[Export("initRuntimeSettings:error:")]
		bool InitRuntimeSettings(string content, [NullAllowed] out NSError error);

		// -(BOOL)initRuntimeSettingsFromFile:(NSString * _Nonnull)filePath error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("initRuntimeSettingsFromFile(_:)")));
		[Export("initRuntimeSettingsFromFile:error:")]
		bool InitRuntimeSettingsFromFile(string filePath, [NullAllowed] out NSError error);

		// -(iDLRRuntimeSettings * _Nullable)getRuntimeSettings:(NSError * _Nullable * _Nullable)error;
		[Export("getRuntimeSettings:")]
		[return: NullAllowed]
		iDLRRuntimeSettings GetRuntimeSettings([NullAllowed] out NSError error);

		// -(BOOL)updateRuntimeSettings:(iDLRRuntimeSettings * _Nonnull)settings error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("updateRuntimeSettings(_:)")));
		[Export("updateRuntimeSettings:error:")]
		bool UpdateRuntimeSettings(iDLRRuntimeSettings settings, [NullAllowed] out NSError error);

		// -(BOOL)resetRuntimeSettings:(NSError * _Nullable * _Nullable)error;
		[Export("resetRuntimeSettings:")]
		bool ResetRuntimeSettings([NullAllowed] out NSError error);

		// -(NSString * _Nullable)outputRuntimeSettings:(NSString * _Nonnull)settingsName error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("outputRuntimeSettings(_:)")));
		[Export("outputRuntimeSettings:error:")]
		[return: NullAllowed]
		string OutputRuntimeSettings(string settingsName, [NullAllowed] out NSError error);

		// -(BOOL)outputRuntimeSettingsToFile:(NSString * _Nonnull)filePath settingsName:(NSString * _Nonnull)settingsName error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("outputRuntimeSettingsToFile(_:settingsName:)")));
		[Export("outputRuntimeSettingsToFile:settingsName:error:")]
		bool OutputRuntimeSettingsToFile(string filePath, string settingsName, [NullAllowed] out NSError error);

		// -(NSArray<iDLRResult *> * _Nullable)recognizeBuffer:(id)imageData error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("recognizeBuffer(_:)")));
		[Export("recognizeBuffer:error:")]
		[return: NullAllowed]
		iDLRResult[] RecognizeBuffer(NSObject imageData, [NullAllowed] out NSError error);

		// -(NSArray<iDLRResult *> * _Nullable)recognizeFile:(NSString * _Nonnull)fileName error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("recognizeFile(_:)")));
		[Export("recognizeFile:error:")]
		[return: NullAllowed]
		iDLRResult[] RecognizeFile(string fileName, [NullAllowed] out NSError error);

		// -(NSArray<iDLRResult *> * _Nullable)recognizeImage:(UIImage * _Nonnull)image error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("recognizeImage(_:)")));
		[Export("recognizeImage:error:")]
		[return: NullAllowed]
		iDLRResult[] RecognizeImage(UIImage image, [NullAllowed] out NSError error);

		// -(NSArray<iDLRResult *> * _Nullable)recognizeFileInMemory:(NSData * _Nonnull)fileBytes error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("recognizeFileInMemory(_:)")));
		[Export("recognizeFileInMemory:error:")]
		[return: NullAllowed]
		iDLRResult[] RecognizeFileInMemory(NSData fileBytes, [NullAllowed] out NSError error);

		// -(BOOL)setModeArgument:(NSString * _Nonnull)modeName index:(NSInteger)index argumentName:(NSString * _Nonnull)argumentName argumentValue:(NSString * _Nonnull)argumentValue error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("setModeArgument(_:index:argumentName:argumentValue:)")));
		[Export("setModeArgument:index:argumentName:argumentValue:error:")]
		bool SetModeArgument(string modeName, nint index, string argumentName, string argumentValue, [NullAllowed] out NSError error);

		// -(NSString * _Nullable)getModeArgument:(NSString * _Nonnull)modeName index:(NSInteger)index argumentName:(NSString * _Nonnull)argumentName error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("getModeArgument(_:index:argumentName:)")));
		[Export("getModeArgument:index:argumentName:error:")]
		[return: NullAllowed]
		string GetModeArgument(string modeName, nint index, string argumentName, [NullAllowed] out NSError error);

		// -(BOOL)updateReferenceRegionFromBarcodeResults:(NSArray * _Nonnull)textResults templateName:(NSString * _Nonnull)templateName error:(NSError * _Nullable * _Nullable)error __attribute__((swift_name("updateReferenceRegionFromBarcodeResults(_:templateName:)")));
		[Export("updateReferenceRegionFromBarcodeResults:templateName:error:")]
		bool UpdateReferenceRegionFromBarcodeResults(NSObject[] textResults, string templateName, [NullAllowed] out NSError error);

		// +(void)appendCharacterModel:(NSString * _Nonnull)name prototxtBuffer:(NSData * _Nonnull)prototxtBuffer txtBuffer:(NSData * _Nonnull)txtBuffer characterModelBuffer:(NSData * _Nonnull)characterModelBuffer __attribute__((swift_name("appendCharacterModel(_:prototxtBuffer:txtBuffer:characterModelBuffer:)")));
		[Static]
		[Export("appendCharacterModel:prototxtBuffer:txtBuffer:characterModelBuffer:")]
		void AppendCharacterModel(string name, NSData prototxtBuffer, NSData txtBuffer, NSData characterModelBuffer);
	}
}
