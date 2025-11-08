using ViteLoq.Application.DTOs.Templates.Nutrition;
using ViteLoq.Application.Interfaces.Templates;
using ViteLoq.Domain.Interfaces;
using ViteLoq.Domain.Templates.Entities;
using ViteLoq.Domain.Templates.Interfaces;

namespace ViteLoq.Application.Services.Templates;

public class NutritionItemService : INutritionItemService
{
    private readonly INutritionItemRepository _nutritionItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NutritionItemService(INutritionItemRepository nutritionItemRepository, IUnitOfWork unitOfWork)
    {
        _nutritionItemRepository = nutritionItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateAsync(CreateNutritionItemDto nutritionItemDto,
        CancellationToken cancellationToken = default)
    {

        var nutritionItem = new NutritionItem();
        nutritionItem.Id = Guid.NewGuid();
        nutritionItem.Name = nutritionItemDto.Name;
        nutritionItem.LocalizedName = nutritionItemDto.LocalizedName;
        nutritionItem.ScientificName = nutritionItemDto.ScientificName;
        nutritionItem.Brand = nutritionItemDto.Brand;
        nutritionItem.IsLiquid = nutritionItemDto.IsLiquid;
        nutritionItem.IsComposite = nutritionItemDto.IsComposite;
        nutritionItem.IsDemo = nutritionItemDto.IsDemo;
        nutritionItem.DefaultWeightInGrams = nutritionItemDto.DefaultWeightInGrams;
        nutritionItem.UnitReference = nutritionItemDto.UnitReference;
        nutritionItem.ExternalSource = nutritionItemDto.ExternalSource;
        nutritionItem.ExternalId = nutritionItemDto.ExternalId;
        nutritionItem.JsonMetadata = nutritionItemDto.JsonMetadata;
        nutritionItem.CountryOfOrigin = nutritionItemDto.CountryOfOrigin;
        nutritionItem.ProcessingDescription = nutritionItemDto.ProcessingDescription;
        nutritionItem.IngredientsText = nutritionItemDto.IngredientsText;
// More fields mapped...
        nutritionItem.EnergyKcal = nutritionItemDto.EnergyKcal;
        nutritionItem.EnergyKj = nutritionItemDto.EnergyKj;
        nutritionItem.Protein = nutritionItemDto.Protein;
        nutritionItem.Carbohydrate = nutritionItemDto.Carbohydrate;
        nutritionItem.Sugar = nutritionItemDto.Sugar;
        nutritionItem.Starch = nutritionItemDto.Starch;
        nutritionItem.Fiber = nutritionItemDto.Fiber;
        nutritionItem.Fat = nutritionItemDto.Fat;
        nutritionItem.SaturatedFat = nutritionItemDto.SaturatedFat;
        nutritionItem.MonounsaturatedFat = nutritionItemDto.MonounsaturatedFat;
        nutritionItem.PolyunsaturatedFat = nutritionItemDto.PolyunsaturatedFat;
        nutritionItem.TransFat = nutritionItemDto.TransFat;
        nutritionItem.CholesterolMg = nutritionItemDto.CholesterolMg;
        nutritionItem.AlcoholGrams = nutritionItemDto.AlcoholGrams;
        nutritionItem.WaterGrams = nutritionItemDto.WaterGrams;
        nutritionItem.CaffeineMg = nutritionItemDto.CaffeineMg;
        nutritionItem.SaltGrams = nutritionItemDto.SaltGrams;
        nutritionItem.SodiumMg = nutritionItemDto.SodiumMg;
        nutritionItem.PotassiumMg = nutritionItemDto.PotassiumMg;

        nutritionItem.VitaminA_Ug = nutritionItemDto.VitaminA_Ug;
        nutritionItem.VitaminD_Ug = nutritionItemDto.VitaminD_Ug;
        nutritionItem.VitaminE_Mg = nutritionItemDto.VitaminE_Mg;
        nutritionItem.VitaminK_Ug = nutritionItemDto.VitaminK_Ug;
        nutritionItem.VitaminC_Mg = nutritionItemDto.VitaminC_Mg;
        nutritionItem.Thiamin_Mg = nutritionItemDto.Thiamin_Mg;
        nutritionItem.Riboflavin_Mg = nutritionItemDto.Riboflavin_Mg;
        nutritionItem.Niacin_Mg = nutritionItemDto.Niacin_Mg;
        nutritionItem.VitaminB6_Mg = nutritionItemDto.VitaminB6_Mg;
        nutritionItem.Folate_Ug = nutritionItemDto.Folate_Ug;
        nutritionItem.VitaminB12_Ug = nutritionItemDto.VitaminB12_Ug;
        nutritionItem.PantothenicAcid_Mg = nutritionItemDto.PantothenicAcid_Mg;
        nutritionItem.Biotin_Ug = nutritionItemDto.Biotin_Ug;

        nutritionItem.CalciumMg = nutritionItemDto.CalciumMg;
        nutritionItem.IronMg = nutritionItemDto.IronMg;
        nutritionItem.MagnesiumMg = nutritionItemDto.MagnesiumMg;
        nutritionItem.PhosphorusMg = nutritionItemDto.PhosphorusMg;
        nutritionItem.ZincMg = nutritionItemDto.ZincMg;
        nutritionItem.SeleniumUg = nutritionItemDto.SeleniumUg;
        nutritionItem.CopperMg = nutritionItemDto.CopperMg;
        nutritionItem.ManganeseMg = nutritionItemDto.ManganeseMg;
        nutritionItem.FluorideMg = nutritionItemDto.FluorideMg;
        nutritionItem.ChlorideMg = nutritionItemDto.ChlorideMg;

        nutritionItem.GlycemicIndex = nutritionItemDto.GlycemicIndex;
        nutritionItem.GlycemicLoad = nutritionItemDto.GlycemicLoad;
        nutritionItem.NetCarbs = nutritionItemDto.NetCarbs;
        nutritionItem.Fructose = nutritionItemDto.Fructose;
        nutritionItem.Lactose = nutritionItemDto.Lactose;
        nutritionItem.Omega3Grams = nutritionItemDto.Omega3Grams;
        nutritionItem.Omega6Grams = nutritionItemDto.Omega6Grams;
        nutritionItem.EPA = nutritionItemDto.EPA;
        nutritionItem.DHA = nutritionItemDto.DHA;
        nutritionItem.CreatineMg = nutritionItemDto.CreatineMg;
        nutritionItem.LysineMg = nutritionItemDto.LysineMg;
        nutritionItem.MethionineMg = nutritionItemDto.MethionineMg;
        nutritionItem.TryptophanMg = nutritionItemDto.TryptophanMg;

        nutritionItem.FlavorProfile = nutritionItemDto.FlavorProfile;
        nutritionItem.Color = nutritionItemDto.Color;
        nutritionItem.TypicalPricePer100Grams = nutritionItemDto.TypicalPricePer100Grams;

        nutritionItem.ThermicEffectPercent = nutritionItemDto.ThermicEffectPercent;

        nutritionItem.BmrImpactKcal = nutritionItemDto.BmrImpactKcal;

        nutritionItem.BmrMultiplier = nutritionItemDto.BmrMultiplier;

        nutritionItem.SatietyScore = nutritionItemDto.SatietyScore;

        nutritionItem.InsulinResponseIndex = nutritionItemDto.InsulinResponseIndex;

        nutritionItem.HormonalImpactNotes = nutritionItemDto.HormonalImpactNotes;


        await _nutritionItemRepository.CreateAsync(nutritionItem);
        await _unitOfWork.SaveChangesAsync();
        return nutritionItem.Id;
    }

    public async Task<NutritionItem> GetByIdAsync(Guid id)
    {
        var nutritionItem = await _nutritionItemRepository.GetByIdAsync(id);
        return nutritionItem;
    }

    public async Task<List<NutritionItem>> GetAllAsync()
    {
        var nutritionItem = await _nutritionItemRepository.GetAllAsync();
        return nutritionItem;
    }
}