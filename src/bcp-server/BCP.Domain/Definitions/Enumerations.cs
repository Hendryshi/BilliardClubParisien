using System.ComponentModel;

namespace BCP.Domain.Definitions
{
	public enum SexEnum
	{
		[Description("Man")]
		M,
		[Description("Woman")]
		F
	}

	public enum FormulaEnum
	{
		[Description("Silver")]
		Silver,
		[Description("Gold")]
		Gold
	}

	public enum CompetitionCategorieEnum
	{
		[Description("Junior Category (-23 years and under)")]
		Junior,
		[Description("\r\nMixed Category")]
		Mixed,
		[Description("Female Category")]
		Female,
		[Description("Regional Level")]
		Regional,
		[Description("National Level")]
		National
	}

	public enum InscriptionStatusEnum
	{
		Pending,
		Refused,
		Accepted
	}
}
