using BCP.Application.Exceptions;
using FluentResults;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace BCP.Application.Extensions
{
	public static class ResultExtensions
	{
		/// <summary>
		/// Adds a new Error to Result
		/// </summary>
		/// <param name="result"></param>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static ResultBase WithError(this ResultBase result, string errorMessage)
		{
			result.Errors.Add(new Error(errorMessage));
			return result;
		}

		/// <summary>
		/// Gives all of the Error messages in a Result
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		public static string ToErrorMessages(this ResultBase result)
		{
			var errorMessages = new List<string>();
			foreach(var error in result.Errors)
			{
				errorMessages.Add(error.ToString());
			}
			return string.Join("\n", errorMessages);
		}

		/// <summary>
		/// Gives a short error message from a Result, intended to be seen by the front end user
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		public static string ToShortErrorMessage(this ResultBase result)
		{
			var errorMessages = new List<string>();
			foreach(var error in result.Errors)
			{
				var messageSlot = error.Message;
				var reasonSlot = "";
				foreach(var errReason in error.Reasons)
				{
					var exceptionProp = errReason.GetType().GetProperty("Exception");
					if(exceptionProp != null)
					{
						var exception = exceptionProp.GetValue(errReason);
						if(exception is Exception ex)
						{
							if(!ex.Message.Equals(messageSlot))
							{
								messageSlot += $": {ex.Message}";
							}
							if(ex.InnerException != null)
							{
								reasonSlot += (string.IsNullOrEmpty(reasonSlot))
									? ex.InnerException.Message
									: ". " + ex.InnerException.Message;
							}
						}
					}
				}

				var completeError = (string.IsNullOrEmpty(reasonSlot))
					? $"{messageSlot}."
					: $"{messageSlot}. Reasons:{reasonSlot}.";

				errorMessages.Add(completeError);
			}
			return string.Join("\n", errorMessages);
		}

		/// <summary>
		/// Gives all of the Error messages in a ValidationResult
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		public static string ToErrorMessages(this ValidationResult result)
		{
			var errorMessages = new List<string>();
			foreach(var error in result.Errors)
			{
				errorMessages.Add(error.ToString());
			}
			return string.Join("\n", errorMessages);
		}

		public static int GetStatusCode(this ResultBase result)
		{
			var code = StatusCodes.Status500InternalServerError;

			if(result.HasException<BadRequestException>())
			{
				code = StatusCodes.Status400BadRequest;
			}
			else if(result.HasException<UnauthorizedAccessException>())
			{
				code = StatusCodes.Status401Unauthorized;
			}
			else if(result.HasException<ForbiddenAccessException>())
			{
				code = StatusCodes.Status403Forbidden;
			}
			else if(result.HasException<NotFoundException>())
			{
				code = StatusCodes.Status404NotFound;
			}
			return code;
		}
	}
}
