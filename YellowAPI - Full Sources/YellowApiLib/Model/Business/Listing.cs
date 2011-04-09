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
using System.Device.Location;
using System.Xml.Serialization;

namespace YellowPages.YellowApi.Business
{
  /// <summary>
  /// A class that contains information about a business.
  /// </summary>
  public class Listing
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    public Listing()
    {
    }

    #region Id PROPERTY

    private string _id = "";

    /// <summary>
    /// Unique ID for that business.
    /// </summary>
    [XmlAttribute( "id" )]
    public string Id 
    {
      get { return _id; }
      set { _id = value ?? ""; }
    }

    #endregion

    #region ParentId PROPERTY

    private string _parentId = "";

    /// <summary>
    /// The ID of a parent business, if any.
    /// </summary>
    [XmlAttribute( "parentId" )]
    public string ParentId 
    {
      get { return _parentId; }
      set { _parentId = value ?? ""; }
    }

    #endregion

    #region IsParent PROPERTY

    /// <summary>
    /// Indicates if this business is the parent entreprise of child dealers.
    /// </summary>
    [XmlAttribute( "isParent" )]    
    public bool IsParent { get; set; }

    #endregion

    #region Name PROPERTY

    private string _name = "";

    /// <summary>
    /// The business name.
    /// </summary>
    [XmlElement( "Name" )]
    public string Name 
    {
      get { return _name; }
      set { _name = value ?? ""; }
    }

    #endregion

    #region Address PROPERTY

    private Address _address = new Address();

    /// <summary>
    /// The business address.
    /// </summary>
    [XmlElement( "Address" )]
    public Address Address 
    { 
      get { return _address; }
      set { _address = value ?? new Address(); }
    }

    #endregion

    #region Coordinates PROPERTY

    private GeoCode _coordinates = new GeoCode();

    /// <summary>
    /// The geological position.
    /// </summary>
    [XmlElement( "GeoCode" )]
    public GeoCode Coordinates
    {
      get { return _coordinates; }
      set { _coordinates = value ?? new GeoCode(); }
    }

    #endregion

    #region Distance PROPERTY

    private double _distance = double.NaN;

    /// <summary>
    /// The distance from a position when a search is made from a specific geological position.
    /// </summary>
    [XmlElement( "Distance" )]
    public double Distance 
    {
      get { return _distance; }
      set { _distance = value; }
    }

    #endregion

    #region Content PROPERTY

    private Content _content = new Content();

    /// <summary>
    /// Lists available information in the business details.
    /// </summary>
    [XmlElement( "Content" )]
    public Content Content 
    {
      get { return _content; }
      set { _content = value ?? new Content(); }
    }

    #endregion
  }
}
