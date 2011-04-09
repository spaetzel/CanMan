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
  /// This class represents a business address in Canada.
  /// </summary>
  public class Address
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    public Address()
    {
    }

    #region City PROPERTY

    private string _city = "";

    /// <summary>
    /// City.
    /// </summary>
    [XmlElement( "City" )]
    public string City
    {
      get { return _city; }
      set { _city = value ?? ""; }
    }

    #endregion

    #region PostalCode PROPERTY

    private string _postalCode = "";

    /// <summary>
    /// Postal code.
    /// </summary>
    [XmlElement( "Pcode" )]
    public string PostalCode
    {
      get { return _postalCode; }
      set { _postalCode = value ?? ""; }
    }

    #endregion

    #region Province PROPERTY

    private string _province = "";

    /// <summary>
    /// Province full name.
    /// </summary>
    [XmlElement( "Prov" )]
    public string Province
    {
      get { return _province; }
      set { _province = value ?? ""; }
    }

    #endregion

    #region Street PROPERTY

    private string _street = "";

    /// <summary>
    /// Street number and name.
    /// </summary>
    [XmlElement( "Street" )]
    public string Street
    {
      get { return _street; }
      set { _street = value ?? ""; }
    }

    #endregion
  }
}
