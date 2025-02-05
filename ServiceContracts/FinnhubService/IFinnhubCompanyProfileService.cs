namespace ServiceContracts.FinnhubService
{
    public interface IFinnhubCompanyProfileService
    {
        /// <summary>
		/// Returns company details such as company name, company logo, country
		/// </summary>
		/// <param name="stockSymbol">Stock symbol to search for</param>
		/// <returns>Returns a dictionary that contains company details</returns>
		public Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
    }
}