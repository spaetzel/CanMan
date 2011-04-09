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
using System.Globalization;
using System.Text;
using System.IO;
using System.Device.Location;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace YellowPages.YellowApi
{
  /// <summary>
  /// This static class gives you access to helper methods for calling the YellowAPI web services. 
  /// Visit http://www.yellowapi.com/ for more information.
  /// </summary>
  public static class YellowApiHelper
  {
    private const string YellowApiSandBoxUrl = "http://api.sandbox.yellowapi.com";
    private const string YellowApiUrl = "http://api.yellowapi.com";

    private const string FindBusinessMethod = "{0}/FindBusiness/?what={1}&where={2}&pg={3}&pgLen={4}&lang={5}&fmt=xml&sflag={6}&apikey={7}&UID={8}";
    private const string GetDetailsMethod = "{0}/GetBusinessDetails/?prov={1}&bus-name={2}&listingId={3}&lang={4}&fmt=xml&apikey={5}&UID={6}";
    private const string GetDealersMethod = "{0}/FindDealer/?pid={1}&pg={2}&pgLen={3}&lang={4}&fmt=xml&apikey={5}&UID={6}";

    static YellowApiHelper()
    {
    }

    #region ApplicationKey STATIC PROPERTY

    private static string __applicationKey = "";

    /// <summary>
    /// Gets or sets your YellowAPI application key. This is required.
    /// </summary>
    /// <remarks>Sign in to http://www.yellowapi.com/ for more information.</remarks>
    public static string ApplicationKey
    {
      get { return Uri.UnescapeDataString( __applicationKey ); }
      set 
      {
        if( value == null )
          __applicationKey = "";
        else
          __applicationKey = Uri.EscapeDataString( value ); 
      }
    }

    #endregion

    #region UserAgent STATIC PROPERTY

    private static string __userAgent = "YellowApiLibWP7 (1.0)";

    /// <summary>
    /// Gets or sets the string that is passed in every call to YellowAPI in the "User-Agent" HTTP header.
    /// </summary>
    public static string UserAgent
    {
      get { return __userAgent; }
      set { __userAgent = value ?? ""; }
    }

    #endregion

    #region UserUniqueID STATIC PROPERTY

    private static string __userUniqueID = "";

    /// <summary>
    /// Gets or sets a string that is passed in every call to YellowAPI to identify your users.
    /// </summary>
    public static string UserUniqueID
    {
      get { return Uri.UnescapeDataString( __userUniqueID ); }
      set 
      {
        if( value == null )
          __userUniqueID = "";
        else
          __userUniqueID = Uri.EscapeDataString( value ); 
      }
    }

    #endregion

    /// <summary>
    /// Gets or sets a boolean value that indicates if your application should use the test YellowAPI 
    /// endpoint instead of the production endpoint.
    /// </summary>
    public static bool UseSandBox { get; set; }

    /// <summary>
    /// Initiate a call to YellowAPI to retrieve businesses that match a search term (what), a location (where) and other optional criteria.
    /// </summary>
    /// <param name="what">The search term to match with businesses. Can be a business name, category, cuisine type, etc. Cannot be null or empty.</param>
    /// <param name="where">The location, in coordinates, around which matching businesses must be located. Cannot be null.</param>
    /// <param name="page">The results' page to get. The minimum value is 1.</param>
    /// <param name="language">The results' language, either English or French.</param>
    /// <param name="flags">Options to finetune your search.</param>
    /// <param name="callback">The method that will be called when the results are fetched. Cannot be null.</param>
    /// <param name="state">An object or value that will be passed back to your callback.</param>
    /// <returns>A YellowApiToken object that lets you abort the operation if it's taking too long.</returns>
    public static YellowApiToken<Business.SearchResults> FindBusinessAsync( 
      string what,
      GeoCoordinate where,
      int page,
      YellowApiLanguage language,
      YellowApiFlags flags,
      YellowApiCallback<Business.SearchResults> callback,
      object state )
    {
      VerifyWhere( where );

      return YellowApiHelper.FindBusinessAsync(
        what,
        string.Format( "cZ{0:F6},{1:F6}", where.Longitude, where.Latitude ),
        page,
        language,
        flags,
        callback,
        state );
    }

    /// <summary>
    /// Initiate a call to YellowAPI to retrieve businesses that match a search term (what), a location (where) and other optional criteria.
    /// </summary>
    /// <param name="what">The search term to match with businesses. Can be a business name, category, cuisine type, etc. Cannot be null or empty.</param>
    /// <param name="where">The city to search matching businesses in. Cannot be null or empty.</param>
    /// <param name="page">The result's page to get. The minimum value is 1.</param>
    /// <param name="language">The results' language, either English or French.</param>
    /// <param name="flags">Options to finetune your search.</param>
    /// <param name="callback">The method that will be called when the results are fetched. Cannot be null.</param>
    /// <param name="state">An object or value that will be passed back to your callback.</param>
    /// <returns>A YellowApiToken object that lets you abort the operation if it's taking too long.</returns>
    public static YellowApiToken<Business.SearchResults> FindBusinessAsync( 
      string what, 
      string where, 
      int page, 
      YellowApiLanguage language, 
      YellowApiFlags flags,
      YellowApiCallback<Business.SearchResults> callback,
      object state )
    {
      VerifyWhat( what );
      VerifyWhere( where );
      VerifyPage( page );
      VerifyLanguage( language );

      // flags are bitmasks, handled as is.
      string url = string.Format( FindBusinessMethod, 
                                  ( YellowApiHelper.UseSandBox ? YellowApiSandBoxUrl : YellowApiUrl ),
                                  Uri.EscapeDataString( what ),
                                  Uri.EscapeDataString( where ),
                                  page.ToString( NumberFormatInfo.InvariantInfo ),
                                  40, // Default.
                                  YellowApiHelper.GetLanguageParameter( language ),
                                  YellowApiHelper.GetFlagsParameter( flags ), 
                                  YellowApiHelper.ApplicationKey,
                                  YellowApiHelper.UserUniqueID );

      try
      {
        HttpWebRequest request = HttpWebRequest.CreateHttp( url );
        YellowApiToken<Business.SearchResults> token = new YellowApiToken<Business.SearchResults>( request, callback, state );

        request.UserAgent = YellowApiHelper.UserAgent;

        request.BeginGetResponse( new AsyncCallback( YellowApiHelper.GenericRequestCompleted<Business.SearchResults> ), token );

        return token;
      }
      catch
      {
        // We let exceptions go through as is.
        throw;
      }
    }

    /// <summary>
    /// Initiate a call to YellowAPI to retrieve extra details about a business.
    /// </summary>
    /// <param name="business">A business listing previous obtained via a call to FindBusinessAsync. Cannot be null.</param>
    /// <param name="language">The details' language, either English or French.</param>
    /// <param name="callback">The method that will be called when the details are fetched. Cannot be null.</param>
    /// <param name="state">An object or value that will be passed back to your callback.</param>
    /// <returns>A YellowApiToken object that lets you abort the operation if it's taking too long.</returns>
    public static YellowApiToken<Business.DetailsResults> GetBusinessDetailsAsync( 
      Business.Listing business, 
      YellowApiLanguage language,
      YellowApiCallback<Business.DetailsResults> callback,
      object state )
    {
      if( business == null )
        throw new ArgumentNullException( "business" );

      return YellowApiHelper.GetBusinessDetailsAsync(
        business.Id,
        business.Name,
        business.Address.Province,
        language,
        callback,
        state );
    }

    /// <summary>
    /// Initiate a call to YellowAPI to retrieve extra details about a business.
    /// </summary>
    /// <param name="id">The business ID to get details for, as specified in a Listing's Id property. Cannot be null or empty.</param>
    /// <param name="name">The business name to get details for, as specified in a listing's Name property. Cannot be null or empty.</param>
    /// <param name="provinceAbbreviation">The business province, as an abbreviation. See <seealso cref="Provinces.Abbreviations"/>. Cannot be null or empty.</param>
    /// <param name="language">The details' language, either English or French.</param>
    /// <param name="callback">The method that will be called when the details are fetched. Cannot be null.</param>
    /// <param name="state">An object or value that will be passed back to your callback.</param>
    /// <returns>A YellowApiToken object that lets you abort the operation if it's taking too long.</returns>
    public static YellowApiToken<Business.DetailsResults> GetBusinessDetailsAsync(
      string id,
      string name,
      string provinceAbbreviation,
      YellowApiLanguage language,
      YellowApiCallback<Business.DetailsResults> callback,
      object state )
    {
      VerifyId( id );
      VerifyName( name );
      VerifyProvinceAbbreviation( provinceAbbreviation );

      string url = string.Format( GetDetailsMethod,
                                  ( YellowApiHelper.UseSandBox ? YellowApiSandBoxUrl : YellowApiUrl ),
                                  Provinces.GetProvinceParameter( provinceAbbreviation, language ),
                                  Uri.EscapeDataString( name.Replace( ' ', '-' ).Normalize() ),
                                  Uri.EscapeDataString( id ), // Just in case
                                  YellowApiHelper.GetLanguageParameter( language ),
                                  YellowApiHelper.ApplicationKey,
                                  YellowApiHelper.UserUniqueID );

      try
      {
        HttpWebRequest request = HttpWebRequest.CreateHttp( url );
        YellowApiToken<Business.DetailsResults> token = new YellowApiToken<Business.DetailsResults>( request, callback, state );

        request.UserAgent = YellowApiHelper.UserAgent;

        request.BeginGetResponse( new AsyncCallback( YellowApiHelper.GenericRequestCompleted<Business.DetailsResults> ), token );

        return token;
      }
      catch
      {
        // We let exceptions go through as is.
        throw;
      }
    }

    /// <summary>
    /// Initiate a call to YellowAPI to retrieve a list of businesses with the same banner as the provided parent business.
    /// </summary>
    /// <param name="business">The parent business listing to get dealers for, previous obtained via a call to FindBusinessAsync. Cannot be null, and its IsParent property must be true.</param>
    /// <param name="page">The result's page to get. The minimum value is 1.</param>
    /// <param name="language">The results' language, either English or French.</param>
    /// <param name="callback">The method that will be called when the results are fetched. Cannot be null.</param>
    /// <param name="state">An object or value that will be passed back to your callback.</param>
    /// <returns>A YellowApiToken object that lets you abort the operation if it's taking too long.</returns>
    public static YellowApiToken<Business.SearchResults> GetBusinessDealersAsync(
      Business.Listing business,
      int page, 
      YellowApiLanguage language,
      YellowApiCallback<Business.SearchResults> callback,
      object state )
    {
      if( business == null )
        throw new ArgumentNullException( "business" );

      if( !business.IsParent )
        throw new ArgumentException( "The business must be a parent business.", "business" );

      return YellowApiHelper.GetBusinessDealersAsync( business.Id, page, language, callback, state );
    }

    /// <summary>
    /// Initiate a call to YellowAPI to retrieve a list of businesses with the same banner as the provided parent business.
    /// </summary>
    /// <param name="id">The parent business ID to get dealers for, as specified in a Listing's Id property. Cannot be null or empty.</param>
    /// <param name="page">The result's page to get. The minimum value is 1.</param>
    /// <param name="language">The results' language, either English or French.</param>
    /// <param name="callback">The method that will be called when the results are fetched. Cannot be null.</param>
    /// <param name="state">An object or value that will be passed back to your callback.</param>
    /// <returns>A YellowApiToken object that lets you abort the operation if it's taking too long.</returns>
    public static YellowApiToken<Business.SearchResults> GetBusinessDealersAsync(
      string id,
      int page,
      YellowApiLanguage language,
      YellowApiCallback<Business.SearchResults> callback,
      object state )
    {
      VerifyId( id );
      VerifyPage( page );
      VerifyLanguage( language );

      string url = string.Format( GetDealersMethod,
                                  ( YellowApiHelper.UseSandBox ? YellowApiSandBoxUrl : YellowApiUrl ),
                                  Uri.EscapeDataString( id ), // Just in case
                                  page.ToString( NumberFormatInfo.InvariantInfo ),
                                  40, // Default.
                                  YellowApiHelper.GetLanguageParameter( language ),
                                  YellowApiHelper.ApplicationKey,
                                  YellowApiHelper.UserUniqueID );

      try
      {
        HttpWebRequest request = HttpWebRequest.CreateHttp( url );
        YellowApiToken<Business.SearchResults> token = new YellowApiToken<Business.SearchResults>( request, callback, state );

        request.UserAgent = YellowApiHelper.UserAgent;

        request.BeginGetResponse( new AsyncCallback( YellowApiHelper.GenericRequestCompleted<Business.SearchResults> ), token );

        return token;
      }
      catch
      {
        // We let exceptions go through as is.
        throw;
      }
    }

    private static void GenericRequestCompleted<T>( IAsyncResult result )
      where T : class
    {
      YellowApiToken<T> token = result.AsyncState as YellowApiToken<T>;

      try
      {
        WebResponse response = token.Request.EndGetResponse( result );

        using( Stream stream = response.GetResponseStream() )
        {
          XmlSerializer serializer = new XmlSerializer( typeof( T ) );
          T results = null;

#if DEBUG_disabled
          string xml = null;

          using( StreamReader streamReader = new StreamReader( stream ) )
          {
            xml = streamReader.ReadToEnd();
          }

          using( StringReader stringReader = new StringReader( xml ) )
          {
            results = ( T )serializer.Deserialize( stringReader );
          }
#else
          results = ( T )serializer.Deserialize( stream );
#endif

          if( token.UserCallback != null )
          {
            token.UserCallback( results, null );
          }
        }
      }
      catch( WebException except )
      {
        // HTTP errors are reported as is.
        if( token.UserCallback != null )
        {
          token.UserCallback( null, except );
        }
      }
      catch( Exception except )
      {
        // Other errors are also reported as is, though they really are unexpected errors.
        if( token.UserCallback != null )
        {
          token.UserCallback( null, except );
        }
      }
    }

    private static string GetFlagsParameter( YellowApiFlags flags )
    {
      StringBuilder result = new StringBuilder();

      // Adding a dash in front blindly, so if new flags are added here, their position in the code
      // doesn't break things. The leading dash is trimmed on return below.
      if( ( flags & YellowApiFlags.BusinessNameOnly ) == YellowApiFlags.BusinessNameOnly )
      {
        result.Append( "-bn" );
      }

      if( ( flags & YellowApiFlags.BusinessHasPhoto ) == YellowApiFlags.BusinessHasPhoto )
      {
        result.Append( "-fto" );
      }

      if( ( flags & YellowApiFlags.BusinessHasVideo ) == YellowApiFlags.BusinessHasVideo )
      {
        result.Append( "-vdo" );
      }

      return result.ToString().TrimStart( '-' );
    }

    private static string GetLanguageParameter( YellowApiLanguage language )
    {
      switch( language )
      {
        case YellowApiLanguage.English:
          return "en";

        case YellowApiLanguage.French:
          return "fr";
      }

      throw new NotSupportedException( "Unknown language." );
    }

    private static void VerifyId( string id )
    {
      if( id == null )
        throw new ArgumentNullException( "id" );

      if( id.Length == 0 )
        throw new ArgumentException( "The id cannot be empty.", "id" );
    }

    private static void VerifyLanguage( YellowApiLanguage language )
    {
      if( ( language != YellowApiLanguage.English ) && ( language != YellowApiLanguage.French ) )
        throw new ArgumentException( "Unknown 'language' parameter value." );
    }

    private static void VerifyName( string name )
    {
      if( name == null )
        throw new ArgumentNullException( "name" );

      if( name.Length == 0 )
        throw new ArgumentException( "The name cannot be empty.", "name" );
    }

    private static void VerifyPage( int page )
    {
      if( page < 1 )
        throw new ArgumentOutOfRangeException( "page", "The 'page' parameter must be greater than 0." );
    }

    private static void VerifyProvinceAbbreviation( string provinceAbbreviation )
    {
      if( provinceAbbreviation == null )
        throw new ArgumentNullException( "provinceAbbreviation" );

      if( provinceAbbreviation.Length == 0 )
        throw new ArgumentException( "The province abbreviation cannot be empty.", "provinceAbbreviation" );
    }

    private static void VerifyWhat( string what )
    {
      if( what == null )
        throw new ArgumentNullException( "what" );

      if( what.Length == 0 )
        throw new ArgumentException( "The 'what' parameter cannot be empty.", "what" );
    }

    private static void VerifyWhere( string where )
    {
      if( where == null )
        throw new ArgumentNullException( "where" );

      if( where.Length == 0 )
        throw new ArgumentException( "The 'where' parameter cannot be empty.", "where" );
    }

    private static void VerifyWhere( GeoCoordinate where )
    {
      if( where == null )
        throw new ArgumentNullException( "where" );

      if( where.IsUnknown )
        throw new ArgumentException( "The search location is unknown.", "where" );
    }

    private static readonly string[] SimpleAsciiEncodingSources = new string[]
      {
        "ÀÁÂÃÄ", "Æ", "Ç", "ÈÉÊË", "ÌÍÎÏĨ", "ÒÓÔÕÖ", "ÙÚÛÜŨ", "àáâãä", "æ", "ç", "èéêë", "ìíîïĩ", "òóôõö", "ùúûüũ", "Œ", "œ"
      };

    private static readonly string[] SimpleAsciiEncodingTargets = new string[]
      {
        "A", "AE", "C", "E", "I", "O", "U", "a", "ae", "c", "e", "i", "o", "u", "OE", "oe"
      };

    private static string Normalize( this string value )
    {
      // Meh. No AsciiEncoding under WP7. We use a simplistic "French/English" compatible solution.
      StringBuilder normalized = new StringBuilder( value.Length );

      foreach( char c in value )
      {
        if( c > 127 )
        {
          bool found = false;

          for( int i = 0; i < SimpleAsciiEncodingSources.Length; i++ )
          {
            if( SimpleAsciiEncodingSources[ i ].IndexOf( c ) != -1 )
            {
              normalized.Append( SimpleAsciiEncodingTargets[ i ] );

              found = true;
              break;
            }
          }

          if( !found )
          {
            System.Diagnostics.Debug.Assert( false, "Asserting for the moment, for tests. Will eventually only debug print." );
            normalized.Append( '-' );
          }
        }
        else
        {
          normalized.Append( c );
        }
      }

      return normalized.ToString();
    }
  }
}
