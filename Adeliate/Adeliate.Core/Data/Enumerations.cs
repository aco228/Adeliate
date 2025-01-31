﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate.Core.Data
{
  public enum GuidType
  {
    Internal = 1,
    External = 2
  }

  public enum IDType
  {
    Internal = 1,
    External = 2
  }

  public enum ImageFormatIdentifier
  {
    Name = 1,
    MimeType = 2,
    Extension = 3
  }

  public enum VideoCodecIdentifier
  {
    Name = 1,
    MimeType = 2,
    Extension = 3
  }

  public enum LookupSessionGuidColumnIdentifier
  {
    LookupSessionGuid = 1,
    IdentificationSessionGuid = 2
  }

  public enum LanguageIdentifier
  {
    GlobalName = 1,
    LocalName = 2,
    Charset = 3,
    TwoLetterIsoCode = 4
  }

  public enum ErrorCode
  {
    Unknown = 0,
    Content = 1,
    Internal = 2,
    Authorization = 3,
    Authentication = 4,
    Service = 5
  }

  public enum CustomerIdentifier
  {
    Msisdn = 1,
    EncryptedMsisdn = 2,
    Username = 3
  }

  public enum PaymentCallbackGuidColumnIdentifier
  {
    Internal = 1,
    Request = 2,
    ExternalRequest = 3
  }

  public enum CountryIdentifier
  {
    Unknown = 0,
    GlobalName = 1,
    LocalName = 2,
    CountryCode = 3,
    CultureCode = 4,
    TwoLetterIsoCode = 5
  }
}
