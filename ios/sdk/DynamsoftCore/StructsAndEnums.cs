using System;
using ObjCRuntime;

namespace DynamsoftCore
{
	[Native]
	public enum EnumDMErrorCode : long
	{
		Ok = 0,
		Unknown = -10000,
		No_Memory = -10001,
		Null_Pointer = -10002,
		License_Invalid = -10003,
		License_Expired = -10004,
		File_Not_Found = -10005,
		Filetype_Not_Supported = -10006,
		BPP_Not_Supported = -10007,
		Image_Read_Failed = -10012,
		TIFF_Read_Failed = -10013,
		PDF_Read_Failed = -10021,
		PDF_DLL_Missing = -10022,
		JSON_Parse_Failed = -10030,
		JSON_Type_Invalid = -10031,
		Json_Key_Invalid = -10032,
		Json_Value_Invalid = -10033,
		Json_Name_Key_Missing = -10034,
		Json_Name_Value_Duplicated = -10035,
		Template_Name_Invalid = -10036,
		Parameter_Value_Invalid = -10038,
		Set_Mode_Argument_Error = -10051,
		Get_Mode_Argument_Error = -10055,
		FileSaveFailed = -10058,
		StageTypeInvalid = -10059,
		ImageOrientationInvalid = -10060,
		No_License = -20000,
		Handshake_Code_Invalid = -20001,
		License_Buffer_Failed = -20002,
		License_Sync_Failed = -20003,
		Device_Not_Match = -20004,
		Bind_Device_Failed = -20005,
		License_Interface_Conflict = -20006,
		License_Client_DLL_Missing = -20007,
		Instance_Count_Over_Limit = -20008,
		License_Init_Sequence_Failed = -20009,
		Trial_License = -20010,
		Failed_To_Reach_DLS = -20200
	}

	[Flags]
	[Native]
	public enum EnumBarcodeFormat : long
	{
		Null = 0x0,
		Code39 = 0x1,
		Code128 = 0x2,
		Code93 = 0x4,
		Codabar = 0x8,
		Itf = 0x10,
		Ean13 = 0x20,
		Ean8 = 0x40,
		Upca = 0x80,
		Upce = 0x100,
		Industrial = 0x200,
		Code39extended = 0x400,
		Msicode = 0x100000,
		Gs1databaromnidirectional = 0x800,
		Gs1databartruncated = 0x1000,
		Gs1databarstacked = 0x2000,
		Gs1databarstackedomnidirectional = 0x4000,
		Gs1databarexpanded = 0x8000,
		Gs1databarexpandedstacked = 0x10000,
		Gs1databarlimited = 0x20000,
		Patchcode = 0x40000,
		Code11 = 0x200000,
		Pdf417 = 0x2000000,
		Qrcode = 0x4000000,
		Datamatrix = 0x8000000,
		Aztec = 0x10000000,
		Maxicode = 0x20000000,
		Microqr = 0x40000000,
		Micropdf417 = 0x80000,
		Gs1composite = -0x80000000L,
		Oned = 0x3007ff,
		Gs1databar = 0x3f800,
		All = -0x1c00001
	}

	[Flags]
	[Native]
	public enum EnumBarcodeFormat2 : long
	{
		Null = 0x0,
		Nonstandardbarcode = 0x1,
		PharmacodeOneTrack = 0x4,
		PharmacodeTwoTrack = 0x8,
		Pharmacode = 0xc,
		Dotcode = 0x2,
		Postalcode = 0x1f00000,
		Uspsintelligentmail = 0x100000,
		Postnet = 0x200000,
		Planet = 0x400000,
		Australianpost = 0x800000,
		Rm4scc = 0x1000000
	}

	[Native]
	public enum EnumImagePixelFormat : long
	{
		Binary = 0,
		BinaryInverted = 1,
		GrayScaled = 2,
		Nv21 = 3,
		Rgb565 = 4,
		Rgb555 = 5,
		Rgb888 = 6,
		Argb8888 = 7,
		Rgb161616 = 8,
		Argb16161616 = 9,
		Abgr8888 = 10,
		Abgr16161616 = 11,
		Bgr888 = 12
	}

	[Native]
	public enum EnumBinarizationMode : long
	{
		Auto = 1,
		LocalBlock = 2,
		Threshold = 4,
		Skip = 0,
		Rev = -2147483648L
	}

	[Native]
	public enum EnumGrayscaleTransformationMode : long
	{
		Inverted = 1,
		Original = 2,
		Auto = 4,
		Skip = 0,
		Rev = -2147483648L
	}

	[Native]
	public enum EnumGrayscaleEnhancementMode : long
	{
		Auto = 1,
		General = 2,
		GrayEqualize = 4,
		GraySmooth = 8,
		SharpenSmooth = 16,
		Skip = 0,
		Rev = -2147483648L
	}

	[Native]
	public enum EnumRegionPredetectionMode : long
	{
		Auto = 1,
		General = 2,
		GeneralRGBContrast = 4,
		GeneralGrayContrast = 8,
		GeneralHSVContrast = 16,
		ManualSpecification = 32,
		Skip = 0,
		Rev = -2147483648L
	}

	[Native]
	public enum EnumScaleUpMode : long
	{
		Auto = 1,
		LinearInterpolation = 2,
		NearestNeighbourInterpolation = 4,
		Skip = 0,
		Rev = -2147483648L
	}

	[Native]
	public enum EnumColourConversionMode : long
	{
		General = 1,
		Skip = 0,
		Rev = -2147483648L
	}

	[Native]
	public enum EnumTextureDetectionMode : long
	{
		Auto = 1,
		GeneralWidthConcentration = 2,
		Skip = 0,
		Rev = -2147483648L
	}

	[Native]
	public enum EnumPDFReadingMode : long
	{
		Auto = 1,
		Vector = 2,
		Raster = 4,
		Rev = -2147483648L
	}
}