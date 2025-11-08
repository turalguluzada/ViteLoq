using ViteLoq.Domain.Base.Entities;

namespace ViteLoq.Domain.Goals.Entities;

public class UserNutritionTarget : BaseEntity
{
        // Identity user id (string GUID) - required
        public Guid AppUserId { get; set; }
        // navigation (opsiyonel, relationship eklediğinde kullanılacak)
        // public AppUser AppUser { get; set; }

        // ------------------------ 
        // Temel günlük hedeflerr
        // ------------------------
        public decimal? TargetCalories { get; set; } // kcal
        public decimal? TargetProteinGrams { get; set; } // g
        public decimal? TargetCarbsGrams { get; set; } // g
        public decimal? TargetFatsGrams { get; set; } // g

        // // Eğer makro yüzdeleri ile çalışmak istersen (0-100)
        // public decimal? TargetProteinPercent { get; set; } // %
        // public decimal? TargetCarbsPercent { get; set; } // %
        // public decimal? TargetFatsPercent { get; set; } // %
        // public decimal? TargetCreatine { get; set; }

        // Mikro ve diğer besin hedefleri (birimler yorum satırlarında)
        public decimal? TargetFiberGrams { get; set; } // g
        public decimal? TargetSugarGrams { get; set; } // g (serbest şeker)
        public decimal? TargetSodiumMg { get; set; } // mg
        public decimal? TargetCholesterolMg { get; set; } // mg
        public decimal? TargetAlcoholGrams { get; set; } // g

        // Water / hydration
        public decimal? TargetWaterLiters { get; set; } // litre
        public int? WaterReminderMl { get; set; } // hatırlatma aralığı (ml) - opsiyonel

        // Vitamin hedefleri (birimler: mg veya μg, yorumda belirt)
        public decimal? TargetVitaminA_Ug { get; set; } // μg RAE
        public decimal? TargetVitaminD_IU { get; set; } // IU (veya μg tercih edilecekse ayrı alan)
        public decimal? TargetVitaminC_Mg { get; set; } // mg
        public decimal? TargetVitaminE_Mg { get; set; } // mg
        public decimal? TargetVitaminK_Ug { get; set; } // μg
        public decimal? TargetVitaminB1_Mg { get; set; } // mg (B1)
        public decimal? TargetVitaminB2_Mg { get; set; } // mg (B2)
        public decimal? TargetVitaminB3_Mg { get; set; } // mg (Niacin)
        public decimal? TargetVitaminB5_Mg { get; set; } // mg (Pantothenic)
        public decimal? TargetVitaminB6_Mg { get; set; } // mg
        public decimal? TargetVitaminB7_Mcg { get; set; } // μg (Biotin)
        public decimal? TargetFolate_Mcg { get; set; } // μg (B9)
        public decimal? TargetVitaminB12_Mcg { get; set; } // μg

        // Mineraller (birimler genel olarak mg veya μg)
        public decimal? TargetCalciumMg { get; set; } // mg
        public decimal? TargetIronMg { get; set; } // mg
        public decimal? TargetMagnesiumMg { get; set; } // mg
        public decimal? TargetPhosphorusMg { get; set; } // mg
        public decimal? TargetPotassiumMg { get; set; } // mg
        public decimal? TargetZincMg { get; set; } // mg
        public decimal? TargetSeleniumMcg { get; set; } // μg
        public decimal? TargetIodineMcg { get; set; } // μg
        public decimal? TargetCopperMg { get; set; } // mg
        public decimal? TargetManganeseMg { get; set; } // mg
        public decimal? TargetChromiumMcg { get; set; } // μg

        // Omega-3 / yağ asitleri
        public decimal? TargetOmega3_Mg { get; set; } // mg (EPA+DHA hedefi)

        // Aktivite / hareket hedefleri
        public int? TargetSteps { get; set; } // adım hedefi / gün
        public int? TargetActiveMinutes { get; set; } // aktif dakika hedefi / gün
        public int? TargetExerciseSessionsPerWeek { get; set; } // haftalık seans
        
        // Mental / akil sagligi
        public decimal? TargetStressLevel { get; set; } // 1-10 arası stres hedefi
        public decimal? TargetSleepQuality { get; set; } // 1-10 arası
        public decimal? TargetMoodScore { get; set; } // 1-10 arası
        
        // Uyku hedefi
        public decimal? TargetSleepHours { get; set; } // saat

        // Ağırlık / hedef kilo
        public decimal? CurrentWeightKg { get; set; } // kg (opsiyonel tekrar)
        public decimal? TargetWeightKg { get; set; } // kg
        public DateTime? TargetWeightDate { get; set; } // hedefe ulaşılacak tarih

        // Metabolic bilgiler (opsiyonel, hesaplanmış)
        public decimal? BMR { get; set; } // Bazal metabolizma (kcal)
        public decimal? TDEE { get; set; } // Toplam günlük enerji harcaması (kcal)

        // Hedef tipi / aktivite seviyesi / birim tercihi
        public string GoalType { get; set; } // "Maintain","Lose","Gain" - veya enum string
        public string ActivityLevel { get; set; } // "Sedentary","Light","Moderate","Active","VeryActive"
        public string UnitSystem { get; set; } // "Metric" veya "Imperial"

        // Calorie management: haftalık değişim hedefi vb.
        public decimal? CalorieAdjustmentPerDay { get; set; } // Günlük kalori artış/azalış hedefi (kcal/gün)
        public decimal? WeeklyWeightChangeKg { get; set; } // kg/week hedef

        // Diğer ayarlar / opsiyonel
        public bool? UseAutoCalculatedTargets { get; set; } // sistem otomatik hesaplasın mı?
        public bool? ReceiveReminders { get; set; } // hatırlatmalar açılsın mı?

        // Extensibility: ileride yeni hedefler eklemek için JSON/storable alanı
        public string AdditionalTargetsJson { get; set; } // serileştirilmiş esnek hedefler (opsiyonel)

        // Notlar
        // public string Notes { get; set; }

        // Soft delete + timestamps
        public bool IsDeleted { get; set; } = false;
}