using System.ComponentModel.DataAnnotations;

namespace EnumComboBox
{
	enum Dogs
	{
		[Display(Name = "Golden Retriver")]
		GoldenRetriver,

		[Display(Name = "Miniture Dachshund")]
		MinitureDachshund,

		[Display(Name = "Border Collie")]
		BorderCollie,
	}
}
