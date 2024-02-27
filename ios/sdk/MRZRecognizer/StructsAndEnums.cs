using System;
using ObjCRuntime;

namespace MRZRecognizer
{
	[Native]
	public enum EnumMRTDDocumentType : long
	{
		All = 0,
		Passsport,
		IdCard,
		Visa
	}

}
