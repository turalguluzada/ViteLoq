namespace ViteLoq.Application.DTOs.Templates.Nutrition;

public class CreateNutritionItemDto
{
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public string Name { get; set; }                       
        public string LocalizedName { get; set; }              // opsiyonel, dil bazlı gösterim
        public string ScientificName { get; set; }             // opsiyonel
        public string Brand { get; set; }                      // paketlenmiş ürünler için marka
        public bool IsLiquid { get; set; } = false;
        public bool IsComposite { get; set; } = false;         // birden fazla içerik içeriyorsa true
        public bool IsDemo { get; set; } = false;

        public decimal DefaultWeightInGrams { get; set; } = 100m; // referans ağırlık (100g veya 100ml)
        public string UnitReference { get; set; } = "g";           // "g" veya "ml"

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

        public string FlavorProfile { get; set; }                // "umami, sweet" vb.
        public string Color { get; set; }                        // opsiyonel
        public decimal TypicalPricePer100Grams { get; set; }     // opsiyonel piyasa verisi

        public decimal ThermicEffectPercent { get; set; }

        public decimal BmrImpactKcal { get; set; }              // 100g başına, metabolizma yoluyla TDEE'ye katkı (kcal)

        public decimal BmrMultiplier { get; set; } = 1m;

        public decimal SatietyScore { get; set; }

        public decimal InsulinResponseIndex { get; set; }

        public string HormonalImpactNotes { get; set; }
}