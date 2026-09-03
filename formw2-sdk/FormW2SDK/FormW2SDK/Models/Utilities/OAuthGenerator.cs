using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FormW2SDK.Models.Utilities
{
    public class OAuthGenerator
    {
        #region GenerateJWS And GetAccessToken
        public static void GenerateJWSAndGetAccessToken(HttpClient client)
        {
            #region Get Credentials from TaxBandits Dev Console (https://sandbox.taxbandits.com/ (Settings ==> API Credentials))
            //Fetch the 3 keys from appspettings.json file
            string userToken = Utility.GetAppSettings("UserToken");
            string clientId = Utility.GetAppSettings("ClientId");
            string clientSecret = Utility.GetAppSettings("ClientSecret");
            #endregion

            if (!string.IsNullOrWhiteSpace(userToken) || !string.IsNullOrWhiteSpace(clientId) || !string.IsNullOrWhiteSpace(clientSecret))
            {
                if (!userToken.Equals("--Your UserToken--") || !clientSecret.Equals("--Your ClientSecret--") || !clientId.Equals("--Your ClientId--"))
                {
                    //Encode the JWS with clientSecret to create a signature
                    var clientSecretSymmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(clientSecret));
                    var signingCredentials = new SigningCredentials(clientSecretSymmetricKey, SecurityAlgorithms.HmacSha256);

                    //Create the required set of claims with appropriate values
                    var claims = new[] {
                                    new Claim(JwtRegisteredClaimNames.Iss, clientId),
                                    new Claim(JwtRegisteredClaimNames.Sub, clientId),
                                    new Claim(JwtRegisteredClaimNames.Aud, userToken),
                                    new Claim(JwtRegisteredClaimNames.Iat, ConvertToUnixEpochTimestamp(DateTime.Now).ToString()),
                               };
                    //Setting the 5 minutes expiry to the JWS
                    var token = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: signingCredentials);

                    //WriteToken method constructs & returns the JWS
                    var jws = new JwtSecurityTokenHandler().WriteToken(token);

                    client.DefaultRequestHeaders.Clear();

                    //Add the JWS constructed to the Authentication header
                    client.DefaultRequestHeaders.Add("Authentication", jws);
                }
                else
                {
                    throw new Exception("Access Token is missing in the request header. Please check appsettings json file and include API credentials.");
                }
            }
            else
            {
                throw new Exception("Access Token is missing in the request header. Please check appsettings json file and include API credentials.");
            }
        }
        #endregion

        #region Convert To Unix Epoch Timestamp        
        /// <summary>
        /// Converts to unix epoch timestamp.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static double ConvertToUnixEpochTimestamp(DateTime date)
        {
            //The difference of current time & the origin (in seconds) as per Unix epoch standard is calculated
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
        #endregion

        #region Construct Headers With Access Token        
        /// <summary>
        /// Constructs the headers with access token.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="accessToken">The access token.</param>
        public static void ConstructHeadersWithAccessToken(HttpClient client, string accessToken)
        {
            client.DefaultRequestHeaders.Clear();

            //Add the JWT along with Bearer keyword to the Authorization header
            var jwtAccessToken = $"Bearer {accessToken}";
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", jwtAccessToken);
        }
        #endregion

        #region Get Public Key from Header
        public static string ConvertToUnixEpochString(DateTime dateTime)
        {
            string unixEpoch = string.Empty;
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            if (dateTime != null && dateTime != DateTime.MinValue)
            {
                //Unix Epoch time calculation
                TimeSpan diff = dateTime.ToUniversalTime() - origin;
                unixEpoch = Math.Floor(diff.TotalSeconds).ToString();
            }
            return unixEpoch;
        }
        #endregion
    }
}
