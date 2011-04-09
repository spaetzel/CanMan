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
  /// A class that represents a phone number.
  /// </summary>
  public class Phone
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    public Phone()
    {
    }

    private string _type = "";

    /// <summary>
    /// The phone type.
    /// </summary>
    [XmlAttribute( "type" )]
    public string Type 
    {
      get { return _type; }
      set { _type = value ?? ""; }
    }

    private string _areaCode = "";

    /// <summary>
    /// The area code.
    /// </summary>
    [XmlElement( ElementName = "NPA" )]
    public string AreaCode 
    {
      get { return _areaCode; }
      set { _areaCode = value ?? ""; }
    }

    private string _exchangeNumber = "";

    /// <summary>
    /// The exchange number, or the three numbers that follow the area code.
    /// </summary>
    [XmlElement( ElementName = "NXX" )]
    public string ExchangeNumber
    {
      get { return _exchangeNumber; }
      set { _exchangeNumber = value ?? ""; }
    }

    private string _listingNumber = "";

    /// <summary>
    /// The listing number, or the last four numbers.
    /// </summary>
    [XmlElement( ElementName = "Num" )]
    public string ListingNumber 
    {
      get { return _listingNumber; }
      set { _listingNumber = value ?? ""; }
    }

    private string _displayNumber = "";

    /// <summary>
    /// The full phone number.
    /// </summary>
    [XmlElement( ElementName = "DisplayNum" )]
    public string DisplayNumber 
    {
      get { return _displayNumber; }
      set { _displayNumber = value ?? ""; }
    }
  }
}
