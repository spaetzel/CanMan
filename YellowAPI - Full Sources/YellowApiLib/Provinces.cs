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
using System.Collections.Generic;

namespace YellowPages.YellowApi
{
  /// <summary>
  /// A static class that provides canadian province and territory names.
  /// </summary>
  public static class Provinces
  {
    private static Dictionary<string, string> EnglishProvinces = new Dictionary<string, string>();
    private static Dictionary<string, string> FrenchProvinces = new Dictionary<string, string>();

    static Provinces()
    {
      // The API under GetBusinessDetails requires us to send back the province name in its long form.
      // Here is the mapping between the short form as found in "Address" and the long form.
      // Important: province names here are already "URL-safe" (no spaces, no accented chars)
      EnglishProvinces.Add( Abbreviations.Alberta, "Alberta" );
      EnglishProvinces.Add( Abbreviations.BritishColumbia, "British-Columbia" );
      EnglishProvinces.Add( Abbreviations.Manitoba, "Manitoba" );
      EnglishProvinces.Add( Abbreviations.NewBrunswick, "New-Brunswick" );
      EnglishProvinces.Add( Abbreviations.NewfoundlandAndLabrador, "Newfoundland-and-Labrador" );
      EnglishProvinces.Add( Abbreviations.NorthwestTerritories, "Northwest-Territories" );
      EnglishProvinces.Add( Abbreviations.NovaScotia, "Nova-Scotia" );
      EnglishProvinces.Add( Abbreviations.Nunavut, "Nunavut" );
      EnglishProvinces.Add( Abbreviations.Ontario, "Ontario" );
      EnglishProvinces.Add( Abbreviations.PrinceEdwardIsland, "Prince-Edward-Island" );
      EnglishProvinces.Add( Abbreviations.Quebec, "Quebec" );
      EnglishProvinces.Add( Abbreviations.Saskatchewan, "Saskatchewan" );
      EnglishProvinces.Add( Abbreviations.Yukon, "Yukon" );
      
      FrenchProvinces.Add( Abbreviations.Alberta, "Alberta" );
      FrenchProvinces.Add( Abbreviations.BritishColumbia, "Colombie-Britannique" );
      FrenchProvinces.Add( Abbreviations.Manitoba, "Manitoba" );
      FrenchProvinces.Add( Abbreviations.NewBrunswick, "Nouveau-Brunswick" );
      FrenchProvinces.Add( Abbreviations.NewfoundlandAndLabrador, "Terre-Neuve-et-Labrador" );
      FrenchProvinces.Add( Abbreviations.NorthwestTerritories, "Territoires-du-Nord-Ouest" );
      FrenchProvinces.Add( Abbreviations.NovaScotia, "Nouvelle-Ecosse" );
      FrenchProvinces.Add( Abbreviations.Nunavut, "Nunavut" );
      FrenchProvinces.Add( Abbreviations.Ontario, "Ontario" );
      FrenchProvinces.Add( Abbreviations.PrinceEdwardIsland, "Ile-du-Prince-Edouard" );
      FrenchProvinces.Add( Abbreviations.Quebec, "Quebec" );
      FrenchProvinces.Add( Abbreviations.Saskatchewan, "Saskatchewan" );
      FrenchProvinces.Add( Abbreviations.Yukon, "Yukon" );
    }

    /// <summary>
    /// A static class that provides canadian province and territory abbreviations.
    /// </summary>
    public static class Abbreviations
    {
      /// <summary>Alberta - AB</summary>
      public const string Alberta = "AB";
      /// <summary>British Columbia - BC</summary>
      public const string BritishColumbia = "BC";
      /// <summary>Manitoba - MB</summary>
      public const string Manitoba = "MB";
      /// <summary>New Brunswick - NB</summary>
      public const string NewBrunswick = "NB";
      /// <summary>Newfoundland and Labrador - NL</summary>
      public const string NewfoundlandAndLabrador = "NL";
      /// <summary>Northwest Territories - NT</summary>
      public const string NorthwestTerritories = "NT";
      /// <summary>Nova Scotia - NS</summary>
      public const string NovaScotia = "NS";
      /// <summary>Nunavut - NU</summary>
      public const string Nunavut = "NU";
      /// <summary>Ontario - ON</summary>
      public const string Ontario = "ON";
      /// <summary>Prince-Edward Island - PE</summary>
      public const string PrinceEdwardIsland = "PE";
      /// <summary>Quebec - QC</summary>
      public const string Quebec = "QC";
      /// <summary>Saskatchewan - SK</summary>
      public const string Saskatchewan = "SK";
      /// <summary>Yukon - YT</summary>
      public const string Yukon = "YT";
    }

    // For the moment, there is no need to make this public.
    internal static string GetProvinceParameter( string abbreviation, YellowApiLanguage language )
    {
      if( abbreviation == null )
        throw new ArgumentNullException( "abbreviation" );

      if( abbreviation.Length == 0 ) 
        throw new ArgumentException( "The abbreviation cannot be empty.", "abbreviation" );

      string name = null;

      if( language == YellowApiLanguage.French )
      {
        if( FrenchProvinces.TryGetValue( abbreviation.ToUpper(), out name ) )
        {
          return name;
        }
      }
      else if( language == YellowApiLanguage.English )
      {
        if( EnglishProvinces.TryGetValue( abbreviation.ToUpper(), out name ) )
        {
          return name;
        }
      }
      else
      {
        throw new ArgumentException( "Unknown language.", "language" );
      }

      System.Diagnostics.Debug.Assert( false, "Province abbreviation not found." );
      return "Canada";
    }
  }
}
