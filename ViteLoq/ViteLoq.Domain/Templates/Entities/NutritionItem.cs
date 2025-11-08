using System;
using System.Collections.Generic;
using ViteLoq.Domain.Base.Entities;

namespace ViteLoq.Domain.Templates.Entities
{
    /// <summary>
    /// Neredeyse tüm olası özellikleri içeren tek sınıf FoodItem.
    /// NOT: Tüm sayısal besin/enerji alanları "100g başına" (veya IsLiquid true ise 100ml başına) saklanır.
    /// Özellik isimleri "Per100" eki içermez — bu konvansiyon burada açıklanmıştır.
    /// </summary>
    public class NutritionItem : BaseEntity
    {
        // Kimlik & temel bilgiler
        // public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }                       // ör: "Kartof"
        public string LocalizedName { get; set; }              // opsiyonel, dil bazlı gösterim
        public string ScientificName { get; set; }             // opsiyonel
        public string Brand { get; set; }                      // paketlenmiş ürünler için marka
        public bool IsLiquid { get; set; } = false;
        public bool IsComposite { get; set; } = false;         // birden fazla içerik içeriyorsa true
        public bool IsDemo { get; set; } = false;

        // Ölçü / referans
        public decimal DefaultWeightInGrams { get; set; } = 100m; // referans ağırlık (100g veya 100ml)
        public string UnitReference { get; set; } = "g";           // "g" veya "ml"
        // public List<ServingSize> ServingSizes { get; set; } = new();

        // Harici referanslar & metadata
        public string ExternalSource { get; set; }               // ör: "USDA"
        public string ExternalId { get; set; }                   // ör: FDC id
        public string JsonMetadata { get; set; }                 // esnek ham metadata (json string)
        public string CountryOfOrigin { get; set; }
        public string ProcessingDescription { get; set; }        // "haşlanmış", "kızarmış", "konserve" vb.
        public string IngredientsText { get; set; }              // bileşenlerin ham metni (kompozit ürünler)
        // public List<string> Tags { get; set; } = new();         // genel etiketler
        // public List<string> Categories { get; set; } = new();   // kategori hiyerarşisi
        // public List<string> Allergens { get; set; } = new();    // ör: "milk","gluten"
        // public bool IsPerishable { get; set; } = false;
        // public TimeSpan? TypicalShelfLife { get; set; }          // tahmini raf ömrü (opsiyonel)

        // -----------------------
        // Makrobesinler (tüm değerler "100g başına" veya IsLiquid ise "100ml başına")
        // Birimler: EnergyKcal için kcal, protein/karb/fat için g; mg/µg birim belirtilmiştir.
        // -----------------------
        public decimal EnergyKcal { get; set; }            // 100g başına kcal
        public decimal EnergyKj { get; set; }              // 100g başına kJ (opsiyonel)
        public decimal Protein { get; set; }               // 100g başına g
        public decimal Carbohydrate { get; set; }          // 100g başına toplam karbonhidrat (g)
        public decimal Sugar { get; set; }                 // 100g başına şeker (g)
        public decimal Starch { get; set; }                // 100g başına nişasta (g)
        public decimal Fiber { get; set; }                 // 100g başına lif (g)
        public decimal Fat { get; set; }                   // 100g başına toplam yağ (g)
        public decimal SaturatedFat { get; set; }          // 100g başına doymuş yağ (g)
        public decimal MonounsaturatedFat { get; set; }    // 100g başına tekli doymamış yağ (g)
        public decimal PolyunsaturatedFat { get; set; }    // 100g başına çoklu doymamış yağ (g)
        public decimal TransFat { get; set; }              // 100g başına trans yağ (g)
        public decimal CholesterolMg { get; set; }         // 100g başına kolesterol (mg)
        public decimal AlcoholGrams { get; set; }          // 100g başına alkol (g) (varsa)
        public decimal WaterGrams { get; set; }            // 100g başına su içeriği (g)
        public decimal CaffeineMg { get; set; }            // 100g başına kafein (mg)
        public decimal SaltGrams { get; set; }             // 100g başına tuz (g) — kolaylık için
        public decimal SodiumMg { get; set; }              // 100g başına sodyum (mg)
        public decimal PotassiumMg { get; set; }           // 100g başına potasyum (mg)

        // Vitaminler (100g başına) - birim adı yoksa yorumda belirtildi
        public decimal VitaminA_Ug { get; set; }           // µg RAE /100g
        public decimal VitaminD_Ug { get; set; }           // µg /100g
        public decimal VitaminE_Mg { get; set; }           // mg /100g
        public decimal VitaminK_Ug { get; set; }           // µg /100g
        public decimal VitaminC_Mg { get; set; }           // mg /100g
        public decimal Thiamin_Mg { get; set; }            // B1 mg /100g
        public decimal Riboflavin_Mg { get; set; }         // B2 mg /100g
        public decimal Niacin_Mg { get; set; }             // B3 mg /100g
        public decimal VitaminB6_Mg { get; set; }          // mg /100g
        public decimal Folate_Ug { get; set; }             // µg /100g
        public decimal VitaminB12_Ug { get; set; }         // µg /100g
        public decimal PantothenicAcid_Mg { get; set; }    // mg /100g
        public decimal Biotin_Ug { get; set; }             // µg /100g

        // Mineraller (100g başına)
        public decimal CalciumMg { get; set; }
        public decimal IronMg { get; set; }
        public decimal MagnesiumMg { get; set; }
        public decimal PhosphorusMg { get; set; }
        public decimal ZincMg { get; set; }
        public decimal SeleniumUg { get; set; }
        public decimal CopperMg { get; set; }
        public decimal ManganeseMg { get; set; }
        public decimal FluorideMg { get; set; }
        public decimal ChlorideMg { get; set; }

        // Ek beslenme / fizyolojik özellikler
        public decimal GlycemicIndex { get; set; }               // GI (0-100)
        public decimal GlycemicLoad { get; set; }                // 100g başına GL
        public decimal NetCarbs { get; set; }                    // 100g başına net karbonhidrat (karb - lif)
        public decimal Fructose { get; set; }                    // 100g başına fruktoz (g)
        public decimal Lactose { get; set; }                     // 100g başına laktoz (g)
        public decimal Omega3Grams { get; set; }                 // 100g başına omega-3 (g)
        public decimal Omega6Grams { get; set; }                 // 100g başına omega-6 (g)
        public decimal EPA { get; set; }                         // 100g başına EPA (g)
        public decimal DHA { get; set; }                         // 100g başına DHA (g)
        public decimal CreatineMg { get; set; }                  // 100g başına kreatin (mg) - et ürünleri
        public decimal LysineMg { get; set; }                    // 100g başına lizinin (mg)
        public decimal MethionineMg { get; set; }                // 100g başına metiyonin (mg)
        public decimal TryptophanMg { get; set; }                // 100g başına triptofan (mg)

        // Duyusal / etiket / ekstra
        public string FlavorProfile { get; set; }                // "umami, sweet" vb.
        public string Color { get; set; }                        // opsiyonel
        public decimal TypicalPricePer100Grams { get; set; }     // opsiyonel piyasa verisi

        // -----------------------
        // BMR & metabolik / fizyolojik etki alanları (100g bağlamında veya genel açıklama)
        // -----------------------
        /// <summary>
        /// Besinin termik etkisi (TEF) yüzde olarak. Tipik değerler: protein ~20-30, karbonhidrat ~5-10, yağ ~0-3.
        /// Yüzde tam sayı olarak saklanır (örn: 20 = %20).
        /// </summary>
        public decimal ThermicEffectPercent { get; set; }

        /// <summary>
        /// Tüketildiğinde BMR (bazal metabolizma) üzerinde tahmini anlık kalorik etki.
        /// Açıklama: Bu, ilgili porsiyonun metabolizması nedeniyle günlük enerji harcamasına eklenen yaklaşık kcal miktarıdır (100g başına).
        /// Bu değer tahmini olup pozitif veya sıfıra yakın olabilir.
        /// </summary>
        public decimal BmrImpactKcal { get; set; }              // 100g başına, metabolizma yoluyla TDEE'ye katkı (kcal)

        /// <summary>
        /// Kısa vadeli BMR değişimini yaklaşık tahmin etmek için çarpan.
        /// Ör: 1.01 = %1 artış. Dikkatle kullanılmalı.
        /// </summary>
        public decimal BmrMultiplier { get; set; } = 1m;

        /// <summary>
        /// Tokluk skoru (ör: 0-10 arası) — tipik porsiyon başına ne kadar doyurucu olduğu.
        /// </summary>
        public decimal SatietyScore { get; set; }

        /// <summary>
        /// İnsülin yanıt indeksi (türetilmiş veya göreli) — yüksek değer daha güçlü insülin yanıtı gösterir (100g başına).
        /// </summary>
        public decimal InsulinResponseIndex { get; set; }

        /// <summary>
        /// Besinin metabolizmayı nasıl etkileyebileceğine dair hormonal/klinik notlar (örn: "insülini artırabilir, ghrelin'i azaltır").
        /// </summary>
        public string HormonalImpactNotes { get; set; }

        /// <summary>
        /// Verilen gram miktarı için istirahat metabolizma hızına (RMR/TDEE) tahmini etkisini hesaplar.
        /// BmrImpactKcal (100g başına) ve TEF kullanılarak yaklaşık kcal değeri döner.
        /// </summary>
        /// 
        // public decimal EstimateBmrEffectForGrams(decimal grams)
        // {
        //     // BmrImpactKcal 100g başına. TEF, yemeğin metabolize edilmesi için harcanan enerjinin yüzdesidir.
        //     decimal baseBmrKcal = BmrImpactKcal * grams / 100m;
        //     decimal tefFromEnergy = EnergyKcal * (ThermicEffectPercent / 100m) * grams / 100m;
        //     return baseBmrKcal + tefFromEnergy;
        // }

        // Yönetim / zaman damgaları
        // public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        // public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        
        // public Guid CreatedByUserId { get; set; }

        
        // Aynı dosyada tutulan yardımcı küçük sınıf — "tek sınıf" fikrini korumak için iç içe tanımlandı
        // public class ServingSize
        // {
        //     public string Label { get; set; }                  // "1 orta", "1 su bardağı"
        //     public decimal WeightInGrams { get; set; }         // gram karşılığı
        //     public decimal MultiplierFromDefault => WeightInGrams / (ParentDefaultWeight ?? 100m);
        //     public string Notes { get; set; }                  // "yaklaşık" vs.
        //
        //     // Ebeveynin referans ağırlığına bağlamak için dahili yardımcı
        //     internal decimal? ParentDefaultWeight { get; set; }
        // }

        // Kullanım kolaylığı için hesaplayıcı yardımcılar (veritabanına eşlenmeyebilir)
        /// <summary>
        /// Verilen gram miktarı için enerji (kcal) döner.
        /// </summary>
        // public decimal GetEnergyForGrams(decimal grams)
        // {
        //     return EnergyKcal * grams / 100m;
        // }

        /// <summary>
        /// Verilen gram için herhangi bir besini (per100 olarak verilen) hesaplar.
        /// </summary>
        // public decimal GetNutrientForGrams(decimal nutrientPer100, decimal grams)
        // {
        //     return nutrientPer100 * grams / 100m;
        // }

        /// <summary>
        /// Serving (porsiyon) eklerken parent default ağırlığını ilişkilendirir.
        /// </summary>
        // public void AddServing(string label, decimal weightInGrams, string notes = null)
        // {
        //     var s = new ServingSize
        //     {
        //         Label = label,
        //         WeightInGrams = weightInGrams,
        //         Notes = notes,
        //         ParentDefaultWeight = this.DefaultWeightInGrams
        //     };
        //     ServingSizes.Add(s);
        // }
    }
}
