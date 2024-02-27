//
//  Dynamsoft Label Recognizer SDK
//
//  Copyright © 2021 Dynamsoft. All rights reserved.
//
#import <UIKit/UIKit.h>
#import <DynamsoftCore/DynamsoftCore.h>
@class DynamsoftCameraEnhancer;


NS_ASSUME_NONNULL_BEGIN

static NSString* const DLRErrorDomain = @"com.dynamsoft.labelrecognizer.error";

/**
 * @name EnumErrorCode
 * @{
 */

typedef NS_ENUM(NSInteger, EnumDLRErrorCode) {
    /**Recognizer timeout*/
    EnumDLRErrorCode_Recognizer_Timeout             = -10026,

    /**Character Model file is not found*/
    EnumDLRErrorCode_Character_Model_File_Not_Found = -10100,
};

/**
 * @}
 */

/**
* Describes the result coordinate type.
* @enum EnumResultCoordinateType
*/
typedef NS_ENUM(NSInteger, EnumResultCoordinateType)
{
    /** Returns the coordinate in pixel value. */
    EnumResultCoordinateTypePixel        = 0x01,

    /** Returns the coordinate as a percentage. */
    EnumResultCoordinateTypePercentage = 0x02
};

/**
* Describes the terminate phase.
* @enum EnumTerminatePhase
*/
typedef NS_ENUM(NSInteger, EnumTerminatePhase)
{
    /** Exits the barcode reading algorithm after the region predetection is done. */
    EnumTerminatePhasePredetected      = 0x01,

    /** Exits the barcode reading algorithm after the region predetection and image pre-processing is done. */
    EnumTerminatePhasePreprocecessed = 0x02,

    /** Exits the barcode reading algorithm after the region predetection, image pre-processing, and image binarization are done. */
    EnumTerminatePhaseBinarized      = 0x04,

    /** Exits the barcode reading algorithm after the region predetection, image pre-processing, image binarization, and barcode localization are done. */
    EnumTerminatePhasesLocalized       = 0x08,

    /** Exits the barcode reading algorithm after the region predetection, image pre-processing, image binarization, barcode localization, and barcode type determining are done. */
    EnumTerminatePhaseDetermined      = 0x10,

    /** Exits the barcode reading algorithm after the region predetection, image pre-processing, image binarization, barcode localization, barcode type determining, and barcode Recognizer are done. */
    EnumTerminatePhaseRecognized      = 0x20
};

/**
* Describes the extended result type.
* @enum EnumResultType
*/
typedef NS_ENUM(NSInteger, EnumResultType)
{
    /** Specifies the standard text. This means the barcode value. */
    EnumResultTypeStandardText     = 0,

    /** Specifies the raw text. This means the text that includes start/stop characters, check digits, etc. */
    EnumResultTypeRawText         = 1,

    /** Specifies all the candidate text. This means all the standard text results decoded from the barcode. */
    EnumResultTypeCandidateText = 2,

    /** Specifies the partial text. This means part of the text result decoded from the barcode. */
    EnumResultTypePartialText     = 3
};

/**
* Describes the QR Code error correction level.
* @enum EnumQRCodeErrorCorrectionLevel
*/
typedef NS_ENUM(NSInteger, EnumQRCodeErrorCorrectionLevel)
{
    /** Error Correction Level H (high) */
    EnumQRCodeErrorCorrectionLevelErrorCorrectionH = 0,
    
    /** Error Correction Level L (low) */
    EnumQRCodeErrorCorrectionLevelErrorCorrectionL = 1,

    /** Error Correction Level M (medium-low) */
    EnumQRCodeErrorCorrectionLevelErrorCorrectionM = 2,

    /** Error Correction Level Q (medium-high) */
    EnumQRCodeErrorCorrectionLevelErrorCorrectionQ = 3
};

/**
* @enum EnumDLRLocalizationSourceType
*
* Describes the localization source type
*
*/
typedef NS_ENUM(NSInteger, EnumLocalizationSourceType)
{
    /** Define the reference region using the manually specified location.*/
    EnumLocalizationSourceTypeManualSpecification = 0x01,

    /** Define the reference region using the result(s) of region predetection process.*/
    EnumLocalizationSourceTypePredetectedRegion = 0x02,

    /** Define the reference region using the barcode info.*/
    EnumLocalizationSourceTypeBarcode = 0x04
};

/**
*  iDLRRegion
*
* Stores the region info.
*
*/
@interface iDLRReferenceRegion : NSObject

/** The source type used to localize the reference region(s)..*/
@property (nonatomic, assign) EnumLocalizationSourceType localizationSourceType;

///**The four points of the quadrilateral.*/
//@property (nonatomic, nullable) NSArray *points;

/**Sets the text area relative to the reference region.*/
@property (nonatomic, nullable) iQuadrilateral *location;

/** Whether to use percentage to measure the coordinate or not.*/
@property (nonatomic, assign) NSInteger regionMeasuredByPercentage;

/** The index of a specific region predetection mode in the regionPredetectionModes parameter.*/
@property (nonatomic, assign) NSInteger regionPredetectionModesIndex;

/** The formats of the barcode in BarcodeFormat group 1.*/
@property (nonatomic, assign) NSInteger barcodeFormatIds;

/** The formats of the barcode in BarcodeFormat group 2.*/
@property (nonatomic, assign) NSInteger barcodeFormatIds_2;

/** The regular express pattern of barcode text.*/
@property (nonatomic, copy, nullable) NSString *barcodeTextRegExPattern;

@end

/**
 * iDLRDictionaryCorrectionThreshold
 *
 * Stores the dictionary correction threshold.
 *
 */
@interface iDLRDictionaryCorrectionThreshold : NSObject

/** The minimum value of word length.*/
@property (nonatomic, assign) NSInteger minWordLength;

/** The maximum value of word length.*/
@property (nonatomic, assign) NSInteger maxWordLength;

/** The threshold for the number of error correction characters.*/
@property (nonatomic, assign) NSInteger threshold;

@end

/**
 * iDLRFurtherModes
 *
 * Stores the FurtherModes
 *
 */

@interface iDLRFurtherModes : NSObject

/**Sets the mode and priority for converting a colour image to a grayscale image.*/
@property (nonatomic, readwrite, nullable) NSArray *colourConversionModes;

/**Sets the mode and priority for the grayscale image conversion.*/
@property (nonatomic, readwrite, nullable) NSArray *grayscaleTransformationModes;

/**Sets the region pre-detection mode for barcodes search.*/
@property (nonatomic, readwrite, nullable) NSArray *regionPredetectionModes;

/**Sets the mode and priority for image preprocessing algorithms.*/
@property (nonatomic, readwrite, nullable) NSArray *grayscaleEnhancementModes;

/**Sets the mode and priority for texture detection.*/
@property (nonatomic, readwrite, nullable) NSArray *textureDetectionModes;

@end

/**
*  iDLRRuntimeSettings
*
* Defines a struct to configure the runtime settings. These settings control the recognizer process.
*
*/
@interface iDLRRuntimeSettings : NSObject

/**Sets the number of the threads the algorithm will use to recognize text.*/
@property (nonatomic, assign) NSInteger maxThreadCount;

/**Timeout */
@property (nonatomic, assign) NSInteger timeout;

/**Sets the name of the CharacterModel.*/
@property (nonatomic, copy, nullable) NSString *characterModelName;

/**Sets the reference region to search for text.*/
@property (nonatomic, nullable) iDLRReferenceRegion *referenceRegion;

/**Sets the text area relative to the reference region.*/
@property (nonatomic, nullable) iQuadrilateral *textArea;

/**Sets the dictionaryPath.*/
@property (nonatomic, copy, nullable) NSString *dictionaryPath;

/**Sets the dictionary correction threshold.*/
@property (nonatomic, nullable) iDLRDictionaryCorrectionThreshold *dictionaryCorrectionThreshold;

/**Sets the mode and priority for binarization.*/
@property (nonatomic, readwrite, nullable) NSArray *binarizationModes;

/**Sets further modes.*/
@property (nonatomic, nullable) iDLRFurtherModes *furtherModes;

@end

/**
*  iDLRCharacterResult
*
* Stores character result.
*
*/
@interface iDLRCharacterResult : NSObject

/**The recognized character with highest confidence.*/
@property (nonatomic, copy, nullable) NSString *characterH;

/**The recognized character with middle confidence.*/
@property (nonatomic, copy, nullable) NSString *characterM;

/**The recognized character with lowest confidence.*/
@property (nonatomic, copy, nullable) NSString *characterL;

/**The location of current character.*/
@property (nonatomic, nullable) iQuadrilateral *location;

/**The recognized character with highest confidence.*/
@property (nonatomic, assign) NSInteger characterHConfidence;

/**The recognized character with middle confidence.*/
@property (nonatomic, assign) NSInteger characterMConfidence;

/**The recognized character with lowest confidence.*/
@property (nonatomic, assign) NSInteger characterLConfidence;

@end

/**
*  iDLRLineResult
*
* Stores line result.
*
*/
@interface iDLRLineResult : NSObject

/**The name of the line specification used to recognize current line result.*/
@property (nonatomic, copy, nullable) NSString *lineSpecificationName;

/**The recognized text, ends by '\0'.*/
@property (nonatomic, copy, nullable) NSString *text;

/**The character model used to recognize the text.*/
@property (nonatomic, copy, nullable) NSString *characterModelName;

/**The localization of current line.*/
@property (nonatomic, nullable) iQuadrilateral *location;

/**The confidence of the result.*/
@property (nonatomic, assign) NSInteger confidence;

/**The character results array.*/
@property (nonatomic, nullable) NSArray<iDLRCharacterResult*>* characterResults;

@end

/**
* iDLRResult
*
* Stores result.
*
*/
@interface iDLRResult : NSObject

/**The name of the reference region used to recognize current result.*/
@property (nonatomic, copy, nullable) NSString *referenceRegionName;

/**The name of the text area used to recognize current result.*/
@property (nonatomic, copy, nullable) NSString *textAreaName;

/**The localization result.*/
@property (nonatomic, nullable) iQuadrilateral *location;

/**The confidence of the result.*/
@property (nonatomic, assign) NSInteger confidence;

/**The line results array.*/
@property (nonatomic, nullable) NSArray<iDLRLineResult*>* lineResults;

/**Page number.*/
@property (nonatomic, assign) NSInteger pageNumber;

@end

/**
 Protocol for a delegate to handle callback when DLR results returned.
 */
@protocol LabelResultListener <NSObject>

/**
 * The callback method for obtaining label recognition results.
 * If ImageSource is correctly configured, the callback is triggered each time when label recognition results are output.
 *
 * @param[in] frameId The ID of the frame.  
 * @param[in] imageData The image data of frame.  
 * @param[in] results Recognized label results of the frame.
 */
- (void)labelResultCallback:(NSInteger)frameId
                  imageData:(nonnull iImageData *)imageData
                    results:(nullable NSArray<iDLRResult *> *)results;

@end

@interface DynamsoftLabelRecognizer : NSObject

/**
 * Initializes DynamsoftLabelRecognizer.
 *
 * @return The instance of DynamsoftLabelRecognizer.
 *
 * @par Remark
 *        Partial of the decoding result will be masked with "*" without a valid license key.
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer;
 *    recognizer = [[DynamsoftLabelRecognizer alloc] init];
 * @endcode
 */
- (instancetype)init;

/**
 * Returns the version info of the SDK.
 *
 * @return The DynamsoftLabelRecognizer SDK version information string.
 *
 * @par Code Snippet:
 * @code
 *    NSString* versionInfo = [DynamsoftLabelRecognizer getVersion];
 * @endcode
 */
+ (NSString *)getVersion;

/**
 * Sets callback function to process text results generated during frame decoding.
 *
 * @param [in] listener Callback instance.
 *
 * @par Code Snippet:
 * @code
     DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
     [recognizer setLabelResultListener:self];
 * @endcode
 */
- (void)setLabelResultListener:(nullable id<LabelResultListener>)listener;

/**
 * Setup ImageSource for continuously obtaining iImageData.
 *
 * @param [in] source An instance of ImageSource.
 */
- (void)setImageSource:(id<ImageSource>)source;


/**
 * Start the video streaming barcode decoding thread.
 *
 * @par Code Snippet:
 * @code
     DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
     [recognizer startScanning];
 * @endcode
*/
- (void)startScanning;

/**
 * Stop the video streaming barcode decoding thread.
 *
 *
 * @par Code Snippet:
 * @code
     DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
     [recognizer stopScanning];
 * @endcode
*/
- (void)stopScanning;

/**
 * @}
 * @name Runtime Functions
 * @{
 */

/**
 * Initialize runtime settings with the _Nullable settings in the given JSON string.
 *
 * @param [in] content A JSON string that represents the content of the settings.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @par Code Snippet:
 * @code
     DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
     NSError __autoreleasing * _Nullable error;
     [recognizer initRuntimeSettings:@"{}" error:&error];
 * @endcode
 */
- (BOOL)initRuntimeSettings:(NSString *)content
                               error:(NSError * _Nullable * _Nullable)error
NS_SWIFT_NAME(initRuntimeSettings(_:));

/**
 * Initializes runtime settings with the settings in the given JSON file.
 *
 * @param [in] filePath The path of the settings file.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information _Nullable .
 *
 * @par Code Snippet:
 * @code
     DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
     NSError __autoreleasing * _Nullable error;
     [barcodeReader initRuntimeSettingsFromFile:@"your template file path" error:&error];
 * @endcode
 */
- (BOOL)initRuntimeSettingsFromFile:(NSString *)filePath
                             error:(NSError * _Nullable * _Nullable)error
NS_SWIFT_NAME(initRuntimeSettingsFromFile(_:));

/**
 * Get runtime settings.
 *
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @return A DLRRuntimeSettings storing current runtime settings.
 *
 * @par Code Snippet:
 * @code
 *   DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *   NSError __autoreleasing *  error;
 *   iDLRRuntimeSettings* settings = [recognizer getRuntimeSettings:&error];
 * @endcode
 */
- (nullable iDLRRuntimeSettings *)getRuntimeSettings:(NSError * _Nullable * _Nullable)error;

/**
 * Update runtime settings with a given struct.
 *
 * @param [in] settings The struct of template settings.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    NSError __autoreleasing *  error;
 *    iDLRRuntimeSettings *settings;
 *    [recognizer updateRuntimeSettings:settings error:&error];
 * @endcode
 */
- (BOOL)updateRuntimeSettings:(nonnull iDLRRuntimeSettings *)settings
                        error:(NSError * _Nullable * _Nullable)error
NS_SWIFT_NAME(updateRuntimeSettings(_:));


/**
 * Reset runtime settings.
 *
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    NSError __autoreleasing *  error;
 *    [recognizer resetRuntimeSettings:&error];
 * @endcode
*/
- (BOOL)resetRuntimeSettings:(NSError * _Nullable * _Nullable)error;


/**
 * Output the runtime settings as a NSString.
 *
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    NSError __autoreleasing *  error;
 *    NSString *settings = [recognizer outputRuntimeSettings:@"your settings name" error:&error];
 * @endcode
*/
- (nullable NSString *)outputRuntimeSettings:(NSString *)settingsName
                              error:(NSError * _Nullable * _Nullable)error
NS_SWIFT_NAME(outputRuntimeSettings(_:));

/**
 * Outputs runtime settings and save it into a settings file (JSON file).
 *
 * @param [in] filePath The output file path which stores settings.
 * @param [in] settingsName The name of the template which is to be output.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @par Code Snippet:
 * @code
 *    NSString *settingsName;
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    NSError __autoreleasing *  error;
 *    [recognizer outputRuntimeSettingsToFile:@"your saving file path" settingsName:@"currentRuntimeSettings" error:&error];
 * @endcode
 */
- (BOOL)outputRuntimeSettingsToFile:(NSString *)filePath
                       settingsName:(NSString *)settingsName
                              error:(NSError * _Nullable * _Nullable)error
NS_SWIFT_NAME(outputRuntimeSettingsToFile(_:settingsName:));



/**
 * @}
 * @name Recognize Functions
 * @{
 */

/**
 * Recognizes text from memory buffer containing image pixels in defined format.
 *
 * @param [in] imageData A struct of iDLRImageData that represents an image.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @return All Label results recognized successfully.
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    iDLRResult *result;
 *    iDLRImageData *imageData = [[iDLRImageData alloc] init];
 *    imageData.bytes = bytes;
 *    imageData.height = height;
 *    imageData.width = width;
 *    imageData.stride = stride;
 *    imageData.format = EnumDLRImagePixelFormatNV21;
 *    NSError __autoreleasing *  error;
 *    result = [recognizer recognizeBuffer:imageData error:&error];
 * @endcode
 */
- (nullable NSArray<iDLRResult *> *)recognizeBuffer:(iImageData *)imageData
                                   error:(NSError * _Nullable * _Nullable)error
NS_SWIFT_NAME(recognizeBuffer(_:));

/**
 * Recognizes text from a specified image file.
 *
 * @param [in] fileName The local path of the file.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @return All Label results recognized successfully.
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    iDLRResult *result;
 *    NSError __autoreleasing *  error;
 *    result = [recognizer recognizeFile:@"your file path" error:&error];
 * @endcode
 */
- (nullable NSArray<iDLRResult *> *)recognizeFile:(NSString *)fileName
                                            error:(NSError *_Nullable *_Nullable)error
NS_SWIFT_NAME(recognizeFile(_:));

/**
 * Decodes barcodes from a UIImage.
 *
 * @param [in] image A UIImage.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @return All Label results recognized successfully.
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    iDLRResult *result;
 *    NSError __autoreleasing *  error;
 *    UIImage *Image = [UIImage imageNamed:@"image"];
 *    result = [recognizer recognizeByImage:@"Image" error:&error];
 * @endcode
 */
- (nullable NSArray<iDLRResult *> *)recognizeImage:(UIImage *)image
                                             error:(NSError *_Nullable *_Nullable)error
NS_SWIFT_NAME(recognizeImage(_:));

/**
 * Decodes barcodes from fileBytes in memory.
 *
 * @param [in] fileBytes The data in memory.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @return All Label results recognized successfully.
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    iDLRResult *result;
 *    NSError __autoreleasing *  error;
 *    UIImage *Image = [UIImage imageNamed:@"image"];
 *    result = [recognizer recognizeFileInMemory:@"Image" error:&error];
 * @endcode
 */
- (nullable NSArray<iDLRResult *> *)recognizeFileInMemory:(NSData *)fileBytes
                                                    error:(NSError *_Nullable *_Nullable)error
NS_SWIFT_NAME(recognizeFileInMemory(_:));


/**
 * Sets the optional argument for a specified mode in Modes parameters.
 *
 * @param [in] modeName The mode parameter name to set argument.
 * @param [in] index The array index of mode parameter to indicate a specific mode.
 * @param [in] argumentName The name of the argument to set.
 * @param [in] argumentValue The value of the argument to set.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @par Remarks:
 *        Check @ref ModesArgument for details
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    iDLRRuntimeSettings *settings;
 *    NSError __autoreleasing * _Nullable error;
 *    [recognizer setModeArgument:@"BinarizationModes" index:0 argumentName:@"EnableFillBinaryVacancy" argumentValue:"1" error:&error];
 * @endcode
 */
- (BOOL)setModeArgument:(NSString* _Nonnull)modeName
                  index:(NSInteger)index
           argumentName:(NSString* _Nonnull)argumentName
          argumentValue:(NSString* _Nonnull)argumentValue
                  error:(NSError* _Nullable * _Nullable)error
NS_SWIFT_NAME(setModeArgument(_:index:argumentName:argumentValue:));

/**
 * Gets the optional argument for a specified mode in Modes parameters.
 *
 * @param [in] modeName The mode parameter name to get argument.
 * @param [in] index The array index of mode parameter to indicate a specific mode.
 * @param [in] argumentName The name of the argument to get.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 * @return the optional argument for a specified mode
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    NSString *argumentValue = [recognizer getModeArgument:@"xx" index:0 argumentName:@"xx" error:&error];
 * @endcode
 */
- (nullable NSString *)getModeArgument:(NSString* _Nonnull)modeName
                                index:(NSInteger)index
                         argumentName:(NSString* _Nonnull)argumentName
                                error:(NSError* _Nullable * _Nullable)error
NS_SWIFT_NAME(getModeArgument(_:index:argumentName:));

/**
 * Recognizes text from memory buffer containing image pixels in defined format.
 *
 * @param [in] textResults DynamsoftBarcodeReader Results
 * @param [in] templateName The template name.
 * @param [in,out] error Input a pointer to an error object. If an error occurs, this pointer is set to an actual error object containing the error information. You may specify nil for this parameter if you do not want the error information.
 *
 * @par Code Snippet:
 * @code
 *    DynamsoftLabelRecognizer *recognizer = [[DynamsoftLabelRecognizer alloc] init];
 *    NSArray<iBarcodeResult*> *barcodeResult;
 *    NSError __autoreleasing *error;
 *    [recognizer updateReferenceRegionFromBarcodeResults:barcodeResult templateName:@"" error:&error];
 * @endcode
 */
- (BOOL)updateReferenceRegionFromBarcodeResults:(NSArray<iBarcodeResult*>*)textResults
                                   templateName:(NSString *)templateName
                                          error:(NSError**)error
NS_SWIFT_NAME(updateReferenceRegionFromBarcodeResults(_:templateName:));

/**
 * Append model.
 *
 * @param [in] name .
 * @param [in] prototxtBuffer .
 * @param [in] txtBuffer .
 * @param [in] characterModelBuffer .
 *
 *
 * @par Code Snippet:
 * @code
 *   [DynamsoftLabelRecognizer appendCharacterModel:@"your model name" prototxtBuffer: txtBuffer: characterModelBuffer: ];
 * @endcode
 */
+ (void)appendCharacterModel:(NSString*)name
              prototxtBuffer:(NSData*)prototxtBuffer
                   txtBuffer:(NSData*)txtBuffer
        characterModelBuffer:(NSData*)characterModelBuffer
NS_SWIFT_NAME(appendCharacterModel(_:prototxtBuffer:txtBuffer:characterModelBuffer:));


/**
 * @}
 */

@end

NS_ASSUME_NONNULL_END
