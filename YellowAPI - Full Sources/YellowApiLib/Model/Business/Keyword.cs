/*
Copyright © 2011, Yellow Pages Group Co.  All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1)	Redistributions of source code must retain a complete copy of this notice, including the copyright notice, this list of conditions and the following disclaimer; and
2)	Neither the name of the Yellow Pages Group Co., nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT OWNER AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace YellowPages.YellowApi.Business
{
  /// <summary>
  /// A base class for any extra textual information that can appear in a business details' profile information.
  /// </summary>
  /// <remarks>See <seealso cref="DetailsResults.Products"/> and <seealso cref="Profile"/>.</remarks>
  public abstract class Keyword
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    protected Keyword()
    {
    }

    #region Value PROPERTY

    private string _value = "";

    /// <summary>
    /// The content for this keyword.
    /// </summary>
    [XmlText]
    public string Value
    {
      get { return _value; }
      set { _value = value ?? ""; }
    }

    #endregion
  }

  /// <summary>
  /// A class that represents opening hours information.
  /// </summary>
  public class OpenHoursKeyword : Keyword
  {
  }

  /// <summary>
  /// A class that represents accepted payment methods.
  /// </summary>
  public class PaymentMethodKeyword : Keyword
  {
  }

  /// <summary>
  /// A class that represents languages spoken at this business location.
  /// </summary>
  public class SpokenLanguageKeyword : Keyword
  {
  }

  /// <summary>
  /// A class that represents indications on how to get to that business.
  /// </summary>
  public class GettingThereKeyword : Keyword
  {
  }

  /// <summary>
  /// A class that represents products and services offered.
  /// </summary>
  public class ProductsAndServicesKeyword : Keyword
  {
  }

  /// <summary>
  /// A class that represents specialties offered.
  /// </summary>
  public class SpecialtiesKeyword : Keyword
  {
  }

  /// <summary>
  /// A class that represents brands carried.
  /// </summary>
  public class BrandCarriedKeyword : Keyword
  {
  }

  /// <summary>
  /// A class that represents types of cuisine at a restaurant.
  /// </summary>
  public class CuisineTypeKeyword : Keyword
  {
  }
}
