using DAL;
using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sunrise.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/Master")]
    public class MasterController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetYearForOrderSummary()
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = db.ExecuteSP("YearMas_Select", para.ToArray(), false);

                List<YearMasterResponse> yearMasterResponses = new List<YearMasterResponse>(); 
                yearMasterResponses = DataTableExtension.ToList<YearMasterResponse>(dt);
                if (yearMasterResponses.Count > 0)
                {
                    yearMasterResponses.Add(new YearMasterResponse
                    {
                        iYearId = int.MaxValue,
                        sYear = "12 Months"
                    });

                    return Ok(new ServiceResponse<YearMasterResponse>
                    {
                        Data = yearMasterResponses.OrderByDescending(p=>p.iYearId).ToList(),
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<YearMasterResponse>
                    {
                        Data = yearMasterResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<YearMasterResponse>
                {
                    Data = new List<YearMasterResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult GetCountryList()
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                DataTable dt = db.ExecuteSP("Country_List", para.ToArray(), false);

                List<CountryMasterResponse> countryMasterResponses = new List<CountryMasterResponse>();
                countryMasterResponses = DataTableExtension.ToList<CountryMasterResponse>(dt);
                if (countryMasterResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<CountryMasterResponse>
                    {
                        Data = countryMasterResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CountryMasterResponse>
                    {
                        Data = countryMasterResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CountryMasterResponse>
                {
                    Data = new List<CountryMasterResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetCityListAutocomplete([FromBody]JObject data)
        {
            CityListAutocompleteResquest cityListAutocompleteResquest = new CityListAutocompleteResquest();

            try
            {
                cityListAutocompleteResquest = JsonConvert.DeserializeObject<CityListAutocompleteResquest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CityListAutocompleteResponse>
                {
                    Data = new List<CityListAutocompleteResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (cityListAutocompleteResquest.sSearch != null)
                    para.Add(db.CreateParam("SearchStr", DbType.String, ParameterDirection.Input, cityListAutocompleteResquest.sSearch));
                else
                    para.Add(db.CreateParam("SearchStr", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (cityListAutocompleteResquest.CountryId != 0)
                    para.Add(db.CreateParam("CountryId", DbType.String, ParameterDirection.Input, cityListAutocompleteResquest.CountryId));
                else
                    para.Add(db.CreateParam("CountryId", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("CityMasAutocomplete_Select", para.ToArray(), false);

                List<CityListAutocompleteResponse> cityListAutocompletes = new List<CityListAutocompleteResponse>();
                cityListAutocompletes = DataTableExtension.ToList<CityListAutocompleteResponse>(dt);
                if (cityListAutocompletes.Count > 0)
                {
                    return Ok(new ServiceResponse<CityListAutocompleteResponse>
                    {
                        Data = cityListAutocompletes,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CityListAutocompleteResponse>
                    {
                        Data = cityListAutocompletes,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CityListAutocompleteResponse>
                {
                    Data = new List<CityListAutocompleteResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetCountryListAutoComplete([FromBody]JObject data)
        {
            CityListAutocompleteResquest countrylistautocompleteresquest = new CityListAutocompleteResquest();

            try
            {
                countrylistautocompleteresquest = JsonConvert.DeserializeObject<CityListAutocompleteResquest>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CityListAutocompleteResponse>
                {
                    Data = new List<CityListAutocompleteResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (countrylistautocompleteresquest.sSearch != null)
                    para.Add(db.CreateParam("SearchStr", DbType.String, ParameterDirection.Input, countrylistautocompleteresquest.sSearch));
                else
                    para.Add(db.CreateParam("SearchStr", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("CountryMasAutocomplete_Select", para.ToArray(), false);

                List<CityListAutocompleteResponse> cityListAutocompletes = new List<CityListAutocompleteResponse>();
                cityListAutocompletes = DataTableExtension.ToList<CityListAutocompleteResponse>(dt);
                if (cityListAutocompletes.Count > 0)
                {
                    return Ok(new ServiceResponse<CityListAutocompleteResponse>
                    {
                        Data = cityListAutocompletes,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CityListAutocompleteResponse>
                    {
                        Data = cityListAutocompletes,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CityListAutocompleteResponse>
                {
                    Data = new List<CityListAutocompleteResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult GetDropdownTypeValueList()
        {
            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                DataTable dt = db.ExecuteSP("Country_List", para.ToArray(), false);

                List<CountryMasterResponse> countryMasterResponses = new List<CountryMasterResponse>();
                countryMasterResponses = DataTableExtension.ToList<CountryMasterResponse>(dt);
                if (countryMasterResponses.Count > 0)
                {
                    return Ok(new ServiceResponse<CountryMasterResponse>
                    {
                        Data = countryMasterResponses,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CountryMasterResponse>
                    {
                        Data = countryMasterResponses,
                        Message = "Something Went wrong.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CountryMasterResponse>
                {
                    Data = new List<CountryMasterResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetCountryFromCountryName([FromBody]JObject data)
        {
            CountryMasterResponse CountryMasterResponse = new CountryMasterResponse();

            try
            {
                CountryMasterResponse = JsonConvert.DeserializeObject<CountryMasterResponse>(data.ToString());
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CountryMasterResponse>
                {
                    Data = new List<CountryMasterResponse>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (CountryMasterResponse.sCountryName != null)
                    para.Add(db.CreateParam("SearchStr", DbType.String, ParameterDirection.Input, CountryMasterResponse.sCountryName));
                else
                    para.Add(db.CreateParam("SearchStr", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("GetCountryFromCountryName_Select", para.ToArray(), false);

                List<CountryMasterResponse> _CountryMasterResponse = new List<CountryMasterResponse>();
                _CountryMasterResponse = DataTableExtension.ToList<CountryMasterResponse>(dt);
                if (_CountryMasterResponse.Count > 0)
                {
                    return Ok(new ServiceResponse<CountryMasterResponse>
                    {
                        Data = _CountryMasterResponse,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<CountryMasterResponse>
                    {
                        Data = _CountryMasterResponse,
                        Message = "No Record Found.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CountryMasterResponse>
                {
                    Data = new List<CountryMasterResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
    }
}
