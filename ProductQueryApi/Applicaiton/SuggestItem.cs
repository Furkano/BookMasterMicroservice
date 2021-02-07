using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
// ReSharper disable All

namespace Applicaiton
{
    public class SuggestItem
    {
        public static string[] Array1 = new[] {"Kitap", "Edebitat", "Roman", "Dünya Edebitayı","Türk Edebiyatı","Kurgu"};
        public static string[] Array2 = new[] {"Kitap", "Eğitim", "Ales", "Kpss","İlk öğretim","orta öğretim"};
        public static string[] Array3 = new[] {"Kitap", "Çocuk", "Öykü", "Roman","Masal","Çocuk Şiir","Çocuk Fantastik"};
        public static string[] Array4 = new[] {"Kitap", "Araştırma", "Tarih", "Türk Tarihi","Dünya Tarihi"};
        public static string[] Array5 = new[] {"Kitap", "Araştırma", "Siyaset", "İnsan Hakları","İdeolojiler"};
        public static string[] Array6 = new[] {"Kitap", "Yemek", "Restaurant", "Meyve","Sebze","Ezogelin"};
        
        public static readonly Dictionary<string, string[]> NewDictionary = new Dictionary<string, string[]>()
        {
            // ReSharper disable once StaticMemberInitializerReferesToMemberBelow
            { "Edebitat", Array1 },
            { "Eğitim", Array2 },
            { "Çocuk", Array3 },
            { "Tarih", Array4 },
            { "Siyaset & İdeolojiler", Array5 },
            { "Yemek", Array6 }
        };

        

        public string[] GetMember(string name)
        {
            var result = NewDictionary.FirstOrDefault(p => p.Key.ToLower() == name.ToLower());
            return result.Value;
        }
    }
    
}
