//
//  DynamsoftCore SDK
//
//  Copyright © 2021 Dynamsoft. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

/**
 * @defgroup Enum Enumerations
 * @{
 */

/**
* Describes the EnumErrorCode
* @enum EnumDMErrorCode
*/
typedef NS_ENUM(NSInteger, EnumDMErrorCode) {
    /**Successful. */
    EnumDMErrorCode_OK                             = 0,

    /**Unknown error. */
    EnumDMErrorCode_Unknown                        = -10000,

    /**Not enough memory to perform the operation. */
    EnumDMErrorCode_No_Memory                      = -10001,

    /**Null pointer */
    EnumDMErrorCode_Null_Pointer                   = -10002,

    /**License invalid*/
    EnumDMErrorCode_License_Invalid                = -10003,
    
    /**License expired*/
    EnumDMErrorCode_License_Expired                = -10004,

    /**File not found*/
    EnumDMErrorCode_File_Not_Found                 = -10005,

    /**The file type is not supported. */
    EnumDMErrorCode_Filetype_Not_Supported         = -10006,

    /**The BPP (Bits Per Pixel) is not supported. */
    EnumDMErrorCode_BPP_Not_Supported              = -10007,
    
    /**Failed to read the image. */
    EnumDMErrorCode_Image_Read_Failed              = -10012,

    /**Failed to read the TIFF image. */
    EnumDMErrorCode_TIFF_Read_Failed               = -10013,
    
    /**Failed to read the PDF image. */
    EnumDMErrorCode_PDF_Read_Failed                = -10021,
    
    /**The PDF DLL is missing. */
    EnumDMErrorCode_PDF_DLL_Missing                = -10022,

    /**Json parse failed*/
    EnumDMErrorCode_JSON_Parse_Failed              = -10030,

    /**Json type invalid*/
    EnumDMErrorCode_JSON_Type_Invalid              = -10031,
    
    /**Json key invalid*/
    EnumDMErrorCode_Json_Key_Invalid               = -10032,

    /**Json value invalid*/
    EnumDMErrorCode_Json_Value_Invalid             = -10033,

    /**Json name key missing*/
    EnumDMErrorCode_Json_Name_Key_Missing          = -10034,

    /**The value of the key "Name" is duplicated.*/
    EnumDMErrorCode_Json_Name_Value_Duplicated     = -10035,
    
    /**Template name invalid*/
    EnumDMErrorCode_Template_Name_Invalid          = -10036,
    
    /**Parameter value invalid*/
    EnumDMErrorCode_Parameter_Value_Invalid        = -10038,
    
    /**Failed to set mode's argument.*/
    EnumDMErrorCode_Set_Mode_Argument_Error        = -10051,

    /**Failed to get mode's argument.*/
    EnumDMErrorCode_Get_Mode_Argument_Error        = -10055,

    EnumDMErrorCode_FILE_SAVE_FAILED               = -10058,

    EnumDMErrorCode_STAGE_TYPE_INVALID             = -10059,

    EnumDMErrorCode_IMAGE_ORIENTATION_INVALID      = -10060,
    
    /**No license.*/
    EnumDMErrorCode_No_License                     = -20000,
    
    /**The handshake code is invalid. */
    EnumDMErrorCode_Handshake_Code_Invalid         = -20001,
    
    /**Failed to read or write license cache. */
    EnumDMErrorCode_License_Buffer_Failed          = -20002,

    /**Falied to synchronize license info wirh license tracking server. */
    EnumDMErrorCode_License_Sync_Failed            = -20003,
    
    /**Device does not match with license buffer. */
    EnumDMErrorCode_Device_Not_Match               = -20004,

    /**Falied to bind device. */
    EnumDMErrorCode_Bind_Device_Failed             = -20005,
    
    /**Interface InitLicenseFromDLS can not be used together with other license initiation interfaces. */
    EnumDMErrorCode_License_Interface_Conflict     = -20006,
    
    /**The license client DLL is missing. */
    EnumDMErrorCode_License_Client_DLL_Missing     = -20007,

    /**Install.*/
    EnumDMErrorCode_Instance_Count_Over_Limit      = -20008,

    /**Interface InitLicenseFromDLS has to be called before creating any SDK objects. */
    EnumDMErrorCode_License_Init_Sequence_Failed   = -20009,
    
    /**Trial License*/
    EnumDMErrorCode_Trial_License                  = -20010,

    /**Failed to reach License Tracking Server.*/
    EnumDMErrorCode_Failed_To_Reach_DLS            = -20200
};

/**
 * Describes the type of the barcode in BarcodeFormat group 1. All the formats can be combined, such as BF_CODE_39 | BF_CODE_128.
 * Note: The barcode format our library will search for is composed of [BarcodeFormat group 1](@ref EnumBarcodeFormat) and [BarcodeFormat group 2](@ref EnumBarcodeFormat2), so you need to specify the barcode format in group 1 and group 2 individually.
 * @enum EnumBarcodeFormat
 */
typedef NS_OPTIONS(NSInteger , EnumBarcodeFormat)
{

    /** No barcode format in BarcodeFormat group 1*/
    EnumBarcodeFormatNULL NS_SWIFT_NAME(Null) = 0x0,
    
	/** Code 39 */
    EnumBarcodeFormatCODE39     		 = 0x1,
	
	/** Code 128 */
    EnumBarcodeFormatCODE128    		 = 0x2,

	/** Code 93 */
    EnumBarcodeFormatCODE93     		 = 0x4,

	/** Codabar */
    EnumBarcodeFormatCODABAR    		 = 0x8,

	/** Interleaved 2 of 5 */
    EnumBarcodeFormatITF        		 = 0x10,

	/** EAN-13 */
    EnumBarcodeFormatEAN13      		 = 0x20,

	/** EAN-8 */
    EnumBarcodeFormatEAN8       		 = 0x40,

	/** UPC-A */
    EnumBarcodeFormatUPCA       		 = 0x80,

	/** UPC-E */
    EnumBarcodeFormatUPCE       		 = 0x100,

	/** Industrial 2 of 5 */
    EnumBarcodeFormatINDUSTRIAL 		 = 0x200,

    /** CODE39 Extended */
    EnumBarcodeFormatCODE39EXTENDED 	 = 0x400,
    
    /** MSI Code */
    EnumBarcodeFormatMSICODE             = 0x100000,

    /**DataBar Omnidirectional*/
    EnumBarcodeFormatGS1DATABAROMNIDIRECTIONAL     = 0x800,
    
    /**DataBar Truncated*/
    EnumBarcodeFormatGS1DATABARTRUNCATED           = 0x1000,
    
    /**DataBar Stacked*/
    EnumBarcodeFormatGS1DATABARSTACKED             = 0x2000,
    
    /**DataBar Stacked Omnidirectional*/
    EnumBarcodeFormatGS1DATABARSTACKEDOMNIDIRECTIONAL = 0x4000,
    
    /**DataBar Expanded*/
    EnumBarcodeFormatGS1DATABAREXPANDED            = 0x8000,
    
    /**DataBar Expaned Stacked*/
    EnumBarcodeFormatGS1DATABAREXPANDEDSTACKED     = 0x10000,
    
    /**DataBar Limited*/
    EnumBarcodeFormatGS1DATABARLIMITED             = 0x20000,
    
    /** Patch code. */
    EnumBarcodeFormatPATCHCODE           = 0x00040000,
    
    /** CODE_11 . */
    EnumBarcodeFormatCODE_11             = 0x200000,
    
	/** PDF417 */
    EnumBarcodeFormatPDF417     		 = 0x02000000,

	/** QRCode */
    EnumBarcodeFormatQRCODE     		 = 0x04000000,

	/** DataMatrix */
    EnumBarcodeFormatDATAMATRIX 		 = 0x08000000,

	/** AZTEC */
    EnumBarcodeFormatAZTEC       		 = 0x10000000,
    
    /**MAXICODE */
    EnumBarcodeFormatMAXICODE            = 0x20000000,
    
    /**Micro QR Code*/
    EnumBarcodeFormatMICROQR             = 0x40000000,

    /**Micro PDF417*/
    EnumBarcodeFormatMICROPDF417         = 0x00080000,
    
    /**GS1 Composite Code*/
    EnumBarcodeFormatGS1COMPOSITE        = -2147483648,

    /** Combined value of BF_CODABAR, BF_CODE_128, BF_CODE_39, BF_CODE_39_Extended, BF_CODE_93, BF_EAN_13, BF_EAN_8, INDUSTRIAL_25, BF_ITF, BF_UPC_A, BF_UPC_E, BF_MSI_CODE*/
    EnumBarcodeFormatONED                = 0x003007FF,
    
    /** Combined value of BF_GS1_DATABAR_OMNIDIRECTIONAL, BF_GS1_DATABAR_TRUNCATED, BF_GS1_DATABAR_STACKED, BF_GS1_DATABAR_STACKED_OMNIDIRECTIONAL, BF_GS1_DATABAR_EXPANDED, BF_GS1_DATABAR_EXPANDED_STACKED, BF_GS1_DATABAR_LIMITED */
    EnumBarcodeFormatGS1DATABAR          = 0x0003F800,
    
	/** All supported formats in BarcodeFormat group 1. */
    EnumBarcodeFormatALL                 = -29360129

};

/**
 * Describes the barcode types in BarcodeFormat group 2.
 * Note: The barcode format our library will search for is composed of [BarcodeFormat group 1](@ref EnumBarcodeFormat) and [BarcodeFormat group 2](@ref EnumBarcodeFormat2), so you need to specify the barcode format in group 1 and group 2 individually.
 * @enum EnumBarcodeFormat2
 */
typedef NS_OPTIONS(NSInteger , EnumBarcodeFormat2)
{
    /** No barcode format in BarcodeFormat group 2 */
    EnumBarcodeFormat2NULL NS_SWIFT_NAME(Null) = 0x00,
    
    /** Nonstandard barcode */
    EnumBarcodeFormat2NONSTANDARDBARCODE     = 0x01,
    
    /** PHARMACODE_ONE_TRACK */
    EnumBarcodeFormat2PHARMACODE_ONE_TRACK    = 0x04,
    
    /** PHARMACODE_ONE_TRACK */
    EnumBarcodeFormat2PHARMACODE_TWO_TRACK    = 0x08,
    
    /** PHARMACODE */
    EnumBarcodeFormat2PHARMACODE              = 0x0C,
    
    /** DotCode Barcode.
     When you set this barcode format, the library will automatically add EnumLocalizationModeStatisticsMarks to EnumLocalizationMode if you don't set it,*/
    EnumBarcodeFormat2DOTCODE                = 0x00000002,
    
    /** Combined value of EnumBarcodeFormat2USPSINTELLIGENTMAIL, EnumBarcodeFormat2POSTNET, EnumBarcodeFormat2PLANET, EnumBarcodeFormat2AUSTRALIANPOST, EnumBarcodeFormat2RM4SCC.
     When you set this barcode format, the library will automatically add EnumLocalizationModeStatisticsPostalCode to EnumLocalizationMode if you don't set it,*/
    EnumBarcodeFormat2POSTALCODE             = 0x01F00000,
    
    /** USPS Intelligent Mail.
     When you set this barcode format, the library will automatically add EnumLocalizationModeStatisticsPostalCode to EnumLocalizationMode if you don't set it,*/
    EnumBarcodeFormat2USPSINTELLIGENTMAIL    = 0x00100000,
    
    /** Postnet.
     When you set this barcode format, the library will automatically add EnumLocalizationModeStatisticsPostalCode to EnumLocalizationMode if you don't set it,*/
    EnumBarcodeFormat2POSTNET                = 0x00200000,
    
    /** Planet.
     When you set this barcode format, the library will automatically add EnumLocalizationModeStatisticsPostalCode to EnumLocalizationMode if you don't set it,*/
    EnumBarcodeFormat2PLANET                 = 0x00400000,
    
    /** Australian Post.
     When you set this barcode format, the library will automatically add EnumLocalizationModeStatisticsPostalCode to EnumLocalizationMode if you don't set it,*/
    EnumBarcodeFormat2AUSTRALIANPOST         = 0x00800000,
    
    /** Royal Mail 4-State Customer Barcode.
     When you set this barcode format, the library will automatically add EnumLocalizationModeStatisticsPostalCode to EnumLocalizationMode if you don't set it,*/
    EnumBarcodeFormat2RM4SCC                 = 0x01000000
};

/**
* Describes the image pixel format.
* @enum EnumImagePixelFormat
*/
typedef NS_ENUM(NSInteger, EnumImagePixelFormat)
{

    /** 0:black, 1:white */
    EnumImagePixelFormatBinary         = 0,

    /** 0:white, 1:black */
    EnumImagePixelFormatBinaryInverted = 1,

    /** 8-bit gray */
    EnumImagePixelFormatGrayScaled     = 2,

    /** NV21 */
    EnumImagePixelFormatNV21            = 3,

    /** 16bit with RGB channel order stored in memory from high to low address*/
    EnumImagePixelFormatRGB_565        = 4,

    /** 16bit with RGB channel order stored in memory from high to low address*/
    EnumImagePixelFormatRGB_555        = 5,

    /** 24bit with RGB channel order stored in memory from high to low address*/
    EnumImagePixelFormatRGB_888        = 6,

    /** 32bit with ARGB channel order stored in memory from high to low address*/
    EnumImagePixelFormatARGB_8888      = 7,

    /** 48bit with RGB channel order stored in memory from high to low address*/
    EnumImagePixelFormatRGB_161616     = 8,

    /** 64bit with ARGB channel order stored in memory from high to low address*/
    EnumImagePixelFormatARGB_16161616   = 9,

    /** 32bit with ABGB channel order stored in memory from high to low address */
    EnumImagePixelFormatABGR_8888       = 10,
    
    /** 64bit with ABGR channel order stored in memory from high to low address*/
    EnumImagePixelFormatABGR_16161616   = 11,
    
    /** 24bit with BGR channel order stored in memory from high to low address*/
    EnumImagePixelFormatBGR_888         = 12
};

/**
* Describes the binarization mode.
* @enum EnumBinarizationMode
*/
typedef NS_ENUM(NSInteger, EnumBinarizationMode)
{
    /** Not supported yet. */
    EnumBinarizationModeAuto        = 0x01,

    /** Binarizes the image based on the local block. Check @ref BM for available argument settings.*/
    EnumBinarizationModeLocalBlock = 0x02,

    /** Performs image binarization based on the given threshold. Check @ref BM for available argument settings.*/
    EnumBinarizationModeThreshold  = 0x04,
    
    /** Skips binarization.*/
    EnumBinarizationModeSkip       = 0x00,
    
    /** Reserved setting for binarization mode.*/
    EnumBinarizationModeRev        = -2147483648
};

/**
* Describes the grayscale transformation mode.
* @enum EnumGrayscaleTransformationMode
*/
typedef NS_ENUM(NSInteger, EnumGrayscaleTransformationMode)
{
    /** Transforms to the inverted grayscale for further reference. This value is recommended for light on dark images. */
    EnumGrayscaleTransformationModeInverted = 0x01,
    
    /** Keeps the original grayscale for further reference. This value is recommended for dark on light images. */
    EnumGrayscaleTransformationModeOriginal = 0x02,
    
    /**Lets the library choose an algorithm automatically for grayscale transformation.*/
    EnumGrayscaleTransformationModeAuto     = 0x04,

    /** Skips grayscale transformation. */
    EnumGrayscaleTransformationModeSkip     = 0x00,
    
    /** Reserved setting for grayscale transformation mode. */
    EnumGrayscaleTransformationModeRev      = -2147483648
};

/**
* Describes the grayscale enhancement mode.
* @enum EnumGrayscaleEnhancementMode
*/
typedef NS_ENUM(NSInteger, EnumGrayscaleEnhancementMode)
{
    /** Not supported yet. */
    EnumGrayscaleEnhancementModeAuto           = 0x01,

    /** Take the unpreprocessed image as the preprocessed result for further reference. */
    EnumGrayscaleEnhancementModeGeneral         = 0x02,

    /** Preprocesses the image using the gray equalization algorithm. Check @ref IPM for available argument settings.*/
    EnumGrayscaleEnhancementModeGrayEqualize  = 0x04,

    /** Preprocesses the image using the gray smoothing algorithm. Check @ref IPM for available argument settings.*/
    EnumGrayscaleEnhancementModeGraySmooth    = 0x08,

    /** Preprocesses the image using the sharpening and smoothing algorithm. Check @ref IPM for available argument settings.*/
    EnumGrayscaleEnhancementModeSharpenSmooth = 0x10,
    
    /** Skips grayscale enhancement */
    EnumGrayscaleEnhancementModeSkip             = 0x00,
    
    /** Reserved setting for grayscale enhancement mode. */
    EnumGrayscaleEnhancementModeRev           = -2147483648
};

/**
* Describes the region predetection mode.
* @enum EnumRegionPredetectionMode
*/
typedef NS_ENUM(NSInteger, EnumRegionPredetectionMode)
{
    /** The library will automatically choose the algorithm to detect region. */
    EnumRegionPredetectionModeAuto                     = 0x01,

    /** Takes the whole image as a region. */
    EnumRegionPredetectionModeGeneral                 = 0x02,

    /** Detects region using the general algorithm based on RGB colour contrast. Check @ref RPM for available argument settings.*/
    EnumRegionPredetectionModeGeneralRGBContrast     = 0x04,

    /** Detects region using the general algorithm based on gray contrast. Check @ref RPM for available argument settings.*/
    EnumRegionPredetectionModeGeneralGrayContrast     = 0x08,

    /** Detects region using the general algorithm based on HSV colour contrast. Check @ref RPM for available argument settings.*/
    EnumRegionPredetectionModeGeneralHSVContrast     = 0x10,
    
    EnumRegionPredetectionModeManualSpecification     = 0x20,

    /** Skips region detection. */
    EnumRegionPredetectionModeSkip                     = 0x00,
    
    /** Reserved setting for  region detection mode. */
    EnumRegionPredetectionModeRev                   = -2147483648
};

/**
 *Describes the scale up mode.
 *@enum EnumScaleUpMode
 */
typedef NS_ENUM(NSInteger,EnumScaleUpMode)
{
    /** The library choose an interpolation method automatically to scale up. */
    EnumScaleUpModeAuto                             = 0x01,
    
    /** Scales up using the linear interpolation method. Check @ref SUM for available argument settings. */
    EnumScaleUpModeLinearInterpolation              = 0x02,
    
    /** Scales up the barcode using the nearest-neighbour interpolation method. Check @ref SUM for available argument settings. */
    EnumScaleUpModeNearestNeighbourInterpolation    = 0x04,
    
    /** Skip the scale-up process.*/
    EnumScaleUpModeSkip                             = 0x00,
    
    /** Reserved setting for scale-up mode. */
    EnumScaleUpModeRev                              = -2147483648
};

/**
* Describes the colour conversion mode.
* @enum EnumColourConversionMode
*/
typedef NS_ENUM(NSInteger, EnumColourConversionMode)
{
    /** Converts a colour image to a grayscale image using the general algorithm. Check @ref CICM for available argument settings.*/
    EnumColourConversionModeGeneral = 0x01,

    /** Skips colour conversion. */
    EnumColourConversionModeSkip     = 0x00,
    
    /** Reserved setting for colour conversion mode. */
    EnumColourConversionModeRev        = -2147483648
};

/**
* Describes the texture detection mode.
* @enum EnumTextureDetectionMode
*/
typedef NS_ENUM(NSInteger, EnumTextureDetectionMode)
{
    /** Not supported yet. */
    EnumTextureDetectionModeAuto = 0X01,

    /** Detects texture using the general algorithm. Check @ref TDM for available argument settings.*/
    EnumTextureDetectionModeGeneralWidthConcentration = 0X02,

    /** Skips texture detection. */
    EnumTextureDetectionModeSkip = 0x00,
    
    /** Reserved setting for texture detection mode. */
    EnumTextureDetectionModeRev = -2147483648
};

/**
*Describes the  PDF reading mode.
*@enum PDFReadingMode
*/
typedef NS_ENUM(NSInteger,EnumPDFReadingMode)
{
    /** Lets the library choose the reading mode automatically.*/
    EnumPDFReadingModeAuto =    0x01,
    
    /** Detects barcode from vector data in PDF file.*/
    EnumPDFReadingModeVector =  0x02,
    
    /** Converts the PDF file to image(s) first, then perform barcode recognition.*/
    EnumPDFReadingModeRaster =  0x04,
    
    /** Reserved setting for PDF reading mode. */
    EnumPDFReadingModeRev    = -2147483648
};

/**
 * @} defgroup Enum Enumerations
 */


/*--------------------------------------------------------------------*/


/**
 * @defgroup IOSInterface Class
 * @{
*/

/**
* Stores the quadrilateral
*
*/
@interface iQuadrilateral : NSObject

/**Four vertexes in a clockwise direction of a quadrilateral. Index 0 represents the left-most vertex. */
@property (nonatomic, nonnull) NSArray* points;

- (BOOL)isPointInQuadrilateral:(CGPoint)point NS_SWIFT_NAME(isPointInQuadrilateral(_:));

- (CGRect)getBoundingRect;

@end

/**
*Stores the region info.
*/
@interface iRegionDefinition : NSObject

/** The coordinate or percentage of the topmost region.
*
* @par Value range:
*         regionMeasuredByPercentage = 0, [0, 0x7fffffff]
*         regionMeasuredByPercentage = 1, [0, 100]
* @par Default value:
*         0
*/
@property (nonatomic, assign) NSInteger regionTop;

/** The coordinate or percentage of the leftmost region.
*
* @par Value range:
*         regionMeasuredByPercentage = 0, [0, 0x7fffffff]
*         regionMeasuredByPercentage = 1, [0, 100]
* @par Default value:
*         0
*/
@property (nonatomic, assign) NSInteger regionLeft;

/** The coordinate or percentage of the rightmost region.
*
* @par Value range:
*         regionMeasuredByPercentage = 0, [0, 0x7fffffff]
*         regionMeasuredByPercentage = 1, [0, 100]
* @par Default value:
*         0
*/
@property (nonatomic, assign) NSInteger regionRight;

/** The coordinate or percentage of the bottommost region.
*
* @par Value range:
*         regionMeasuredByPercentage = 0, [0, 0x7fffffff]
*         regionMeasuredByPercentage = 1, [0, 100]
* @par Default value:
*         0
*/
@property (nonatomic, assign) NSInteger regionBottom;

/** Sets whether to use percentages to measure the region size.
*
* @par Value range:
*         [0, 1]
* @par Default value:
*         0
* @par Remarks:
*     When it's set to 1, the values of Top, Left, Right, Bottom indicate the percentage (from 0 to 100); otherwise, they refer to the coordinates.
*     0: not by percentage
*     1: by percentage
*/
@property (nonatomic, assign) NSInteger regionMeasuredByPercentage;

@end

/**
* Stores the image data.
*
*/
@interface iImageData : NSObject

/** The image data content in a byte array */
@property (nonatomic, nullable) NSData* bytes;

/** The width of the image in pixels */
@property (nonatomic, assign) NSInteger width;

/** The height of the image in pixels */
@property (nonatomic, assign) NSInteger height;

/** The stride (or scan width) of the image  */
@property (nonatomic, assign) NSInteger stride;

/** The image pixel format used in the image byte array */
@property (nonatomic, assign) EnumImagePixelFormat format;

@property (nonatomic, assign) NSInteger orientation;

- (UIImage * _Nullable)toUIImage:(NSError *_Nullable *_Nullable)error;

@end

/**
* Stores the barcoderesult
*
*/
@interface iBarcodeResult : NSObject

/** The barcode format in BarcodeFormat group 1 */
@property (nonatomic, assign) EnumBarcodeFormat barcodeFormat;

/** Barcode type in BarcodeFormat group 2 */
@property (nonatomic, assign) EnumBarcodeFormat2 barcodeFormat_2;

/** The barcode text, ends by '\0' */
@property (nonatomic, nullable) NSString* text;

/** The barcode content in a byte array */
@property (nonatomic, nullable) NSData* bytes;

/** The corresponding localization result */
@property (nonatomic, nullable) iQuadrilateral* location;

@property (nonatomic, assign) NSInteger moduleSize;

@property (nonatomic, assign) NSInteger pageNumber;

@end

@interface DynamsoftLicenseManager:NSObject

+ (void)initLicense:(NSString* _Nonnull)license verificationDelegate:(id _Nullable)connectionDelegate;

@end


@protocol LicenseVerificationListener <NSObject>

@required

- (void)licenseVerificationCallback:(bool)isSuccess error:(NSError * _Nullable)error;

@end

@protocol ImageSource <NSObject>

@required

- (iImageData *_Nullable)getImage;

@end

/**
 * @} defgroup IOSInterface Class
*/
