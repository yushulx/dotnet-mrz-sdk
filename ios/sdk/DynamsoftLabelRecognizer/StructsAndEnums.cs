using System;
using ObjCRuntime;

namespace Com.Dynamsoft.Dlr
{
	
	[Native]
	public enum EnumDLRErrorCode : long
	{
		Recognizer_Timeout = -10026,
		Character_Model_File_Not_Found = -10100
	}

	[Native]
	public enum EnumResultCoordinateType : long
	{
		ixel = 1,
		ercentage = 2
	}

	[Native]
	public enum EnumTerminatePhase : long
	{
		Predetected = 1,
		Preprocecessed = 2,
		Binarized = 4,
		sLocalized = 8,
		Determined = 16,
		Recognized = 32
	}

	[Native]
	public enum EnumResultType : long
	{
		StandardText = 0,
		RawText = 1,
		CandidateText = 2,
		PartialText = 3
	}

	[Native]
	public enum EnumQRCodeErrorCorrectionLevel : long
	{
		H = 0,
		L = 1,
		M = 2,
		Q = 3
	}

	[Native]
	public enum EnumLocalizationSourceType : long
	{
		ManualSpecification = 1,
		PredetectedRegion = 2,
		Barcode = 4
	}
}
