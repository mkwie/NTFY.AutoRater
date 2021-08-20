using NTFY.AutoRater.ApiModels;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NTFY.AutoRater
{
    public class NTFYHttpClient
    {
        private readonly HttpClient _httpClient;

        public NTFYHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://dccore.ntfy.pl/");
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var content = GetJsonContent(request);

            var response = await _httpClient.PostAsync("api/login_check", content);
            response.EnsureSuccessStatusCode();

            var tokens = await GetResponse<LoginResponse>(response);

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokens.token);

            return tokens;
        }

        public async Task<DietHistoryResponse> FetchHistory()
        {
            var url = "frontend/secure/diet/fetch-history?page=1";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var dietHistory = await GetResponse<DietHistoryResponse>(response);
            return dietHistory;
        }

        public async Task<CalendarResponse> GetCalendar(DateTime calendarStart, int dietId)
        {
            var dateString = calendarStart.ToString("yyyy-MM-dd");

            var response = await _httpClient.GetAsync($"frontend/secure/calendar/{dietId}/{dateString}.json");
            response.EnsureSuccessStatusCode();

            var calendar = await GetResponse<CalendarResponse>(response);

            return calendar;
        }

        public async Task<BagDetailsResponse> GetDetails(int bagId)
        {
            var response = await _httpClient.GetAsync($"frontend/secure/bag/details/{bagId}.json");
            response.EnsureSuccessStatusCode();

            var bagDetails = await GetResponse<BagDetailsResponse>(response);

            return bagDetails;

            //https://dccore.ntfy.pl/frontend/secure/bag/details/5599234.json
            //{"bag":{"id":5599234,"deliveryDate":"2021-08-16","address":{"username":"Mateusz Kwieci\u0144ski","city":"Warszawa","postCode":"04-072","street":"Kirasjer\u00f3w","buildingNr":"8","placeNr":"22","deliveryHourFrom":null,"deliveryHourTo":"1970-01-01T05:00:00+01:00"}},"mealTypes":[{"id":1,"name":"\u015aniadanie","selected":{"id":10534,"name":"Malinowa granola z ziarnami, jogurt naturalny, nektarynka #LG","variantId":19,"variantName":"Smart","bagItemId":32604583,"rate":0,"comment":"","sizeName":"L","calorific":"500"},"items":[{"id":10534,"name":"Malinowa granola z ziarnami, jogurt naturalny, nektarynka #LG","variantId":19,"variantName":"Smart","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"500"},{"id":10532,"name":"Nale\u015bniki pe\u0142noziarniste z twaro\u017ckiem pomidorowym, rukol\u0105 i szynk\u0105 z indyka, s\u0142upki warzyw","variantId":12,"variantName":"Classic","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"500"},{"id":9303,"name":"Wega\u0144ski twaro\u017cek z orzech\u00f3w nerkowca ze szczypiorkiem, chleb razowy ze s\u0142onecznikiem, \u015bwie\u017ce warzywa #VGAN, #DF","variantId":13,"variantName":"Vegan","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"500"},{"id":5221,"name":"Jaglanka z orzechami w\u0142oskimi, morel\u0105 i czerwon\u0105 porzeczk\u0105 #VGAN, #LG, #DF","variantId":14,"variantName":"Veggy","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"500"},{"id":8189,"name":"Pieczona czekoladowa owsianka z wi\u015bniami, mus brzoskwiniowo-morelowy, winogrona #LG, #DF","variantId":20,"variantName":"Balance","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"500"},{"id":6833,"name":"Omlet z chorizo, papryk\u0105 i szczypiorkiem, ostry sos pomidorowy, rukola z rzodkiewk\u0105 #LG, #DF, #LC","variantId":15,"variantName":"Active","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"500"}],"position":1,"editable":false,"packageId":3,"bagItemId":32604583},{"id":2,"name":"Drugie \u015bniadanie","selected":{"id":853,"name":"Kanapka razowa z zio\u0142owym serkiem, sa\u0142at\u0105 i szynk\u0105, rzodkiewka","variantId":19,"variantName":"Smart","bagItemId":32604584,"rate":0,"comment":"","sizeName":"L","calorific":"300"},"items":[{"id":853,"name":"Kanapka razowa z zio\u0142owym serkiem, sa\u0142at\u0105 i szynk\u0105, rzodkiewka","variantId":19,"variantName":"Smart","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"300"},{"id":8022,"name":"Fit monte z musem truskawkowym #LG","variantId":12,"variantName":"Classic","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"300"},{"id":9308,"name":"Sa\u0142atka z komosy ry\u017cowej i czarnej fasoli z marchewk\u0105 i kolendr\u0105, sos limonkowy z chili #VGAN, #DF","variantId":13,"variantName":"Vegan","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"300"},{"id":9815,"name":"Ciasto pistacjowo-cytrynowe","variantId":14,"variantName":"Veggy","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"300"},{"id":8182,"name":"Smoothie: jab\u0142ko, banan, szpinak #VGAN, #LG, #DF","variantId":20,"variantName":"Balance","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"300"},{"id":9839,"name":"Fit sernik czekoladowo-truskawkowy od MrFit Trenera #LG, #LC","variantId":15,"variantName":"Active","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"300"}],"position":2,"editable":false,"packageId":3,"bagItemId":32604584},{"id":3,"name":"Obiad","selected":{"id":7684,"name":"Indyk duszony z marchewk\u0105 i groszkiem, puree ziemniaczane, sur\u00f3wka z bia\u0142ej kapusty #LG","variantId":19,"variantName":"Smart","bagItemId":32604585,"rate":0,"comment":"","sizeName":"L","calorific":"600"},"items":[{"id":7684,"name":"Indyk duszony z marchewk\u0105 i groszkiem, puree ziemniaczane, sur\u00f3wka z bia\u0142ej kapusty #LG","variantId":19,"variantName":"Smart","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"600"},{"id":2270,"name":"Chili con carne, ry\u017c br\u0105zowy #LG","variantId":12,"variantName":"Classic","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"600"},{"id":10119,"name":"Wega\u0144skie kotlety mielone z pieczarek i seitana w sosie koperkowym, kasza p\u0119czak, og\u00f3rek ma\u0142osolny #VGAN, #DF","variantId":13,"variantName":"Vegan","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"600"},{"id":3529,"name":"Burger z czarnej fasoli i quinoa z sosem BBQ, ziemniaki pieczone, sur\u00f3wka coleslaw #DF","variantId":14,"variantName":"Veggy","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"600"},{"id":10123,"name":"Ciecierzyca w kremowym sosie z nerkowc\u00f3w z indyjskimi przyprawami, ry\u017c ja\u015bminowy z kozieradk\u0105, sur\u00f3wka z kalarepy #VGAN, #LG, #DF","variantId":20,"variantName":"Balance","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"600"},{"id":9816,"name":"Pier\u015b kurczaka w sosie chrzanowym z koperkiem, kasza gryczana, og\u00f3rek ma\u0142osolny #LC, #LG","variantId":15,"variantName":"Active","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"600"}],"position":3,"editable":false,"packageId":3,"bagItemId":32604585},{"id":4,"name":"Podwieczorek","selected":{"id":7689,"name":"Ciasto zebra #DF","variantId":19,"variantName":"Smart","bagItemId":32604586,"rate":0,"comment":"","sizeName":"L","calorific":"200"},"items":[{"id":7689,"name":"Ciasto zebra #DF","variantId":19,"variantName":"Smart","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"200"},{"id":8683,"name":"Sa\u0142atka z kaszy bulgur, pieczonej marchewki i roszponki, sos tahini #DF","variantId":12,"variantName":"Classic","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"200"},{"id":9395,"name":"Kokosowy pudding chia z sosem z owoc\u00f3w le\u015bnych #VGAN, #DF","variantId":13,"variantName":"Vegan","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"200"},{"id":986,"name":"Trufle kokosowe #VGAN, #LG, #DF","variantId":20,"variantName":"Balance","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"200"},{"id":5789,"name":"Koktajl malinowe love #DF, #LC, #LG","variantId":15,"variantName":"Active","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"200"},{"id":8783,"name":"Kartoflanka z grzybami le\u015bnymi #LG","variantId":14,"variantName":"Veggy","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"200"}],"position":4,"editable":false,"packageId":3,"bagItemId":32604586},{"id":5,"name":"Kolacja","selected":{"id":10113,"name":"Nasi goreng czyli indonezyjski ry\u017c z tofu, kapust\u0105 peki\u0144sk\u0105, boczniakami i jajkiem sadzonym #DF, #LG","variantId":20,"variantName":"Balance","bagItemId":32604587,"rate":0,"comment":"","sizeName":"L","calorific":"400"},"items":[{"id":1429,"name":"Cannelloni z kurczakiem, szpinakiem i \u017c\u00f3\u0142tym serem w sosie pomidorowym, sa\u0142atka z sosem winegret","variantId":12,"variantName":"Classic","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"400"},{"id":2076,"name":"Sa\u0142atka z kasz\u0105 gryczan\u0105, w\u0119dzonym twarogiem i pestkami dyni, sos winegret #LG","variantId":14,"variantName":"Veggy","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"400"},{"id":10113,"name":"Nasi goreng czyli indonezyjski ry\u017c z tofu, kapust\u0105 peki\u0144sk\u0105, boczniakami i jajkiem sadzonym #DF, #LG","variantId":20,"variantName":"Balance","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"400"},{"id":7396,"name":"Sa\u0142atka z niebieskim serem ple\u015bniowym i boczkiem, sos winegret #LC, #LG","variantId":15,"variantName":"Active","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"400"},{"id":6567,"name":"Grzybowe curry z czerwon\u0105 soczewic\u0105, makaron sojowy #VGAN, #LG, #DF","variantId":13,"variantName":"Vegan","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"400"},{"id":8773,"name":"Chili z drobiowym mi\u0119sem mielonym, ry\u017c bia\u0142y z groszkiem #LG, #DF","variantId":19,"variantName":"Smart","bagItemId":null,"rate":0,"comment":"","sizeName":"L","calorific":"400"}],"position":5,"editable":false,"packageId":3,"bagItemId":32604587}],"editable":false,"isDelivered":true,"canChangeDeliveryTime":{"possible":false,"disabledDates":[],"disabledDays":[],"deadlineAt":null},"canRateFrom":null,"canRateTo":"2021-08-20T23:59:00+02:00","canRate":true,"canChangeMenuTo":"2021-08-14T16:00:00+02:00","canChangeMenu":false,"canVisibleMealsTo":"2021-08-20T23:59:00+02:00","canVisibleMeals":true,"isRated":false,"canAddElements":false}
        }

        public async Task<HttpResponseMessage> Rate(int bagId, RateRequest request)
        {
            var requestContent = GetJsonContent(request);
            var response = await _httpClient.PostAsync($"frontend/secure/bag/rate/{bagId}.json", requestContent);
            response.EnsureSuccessStatusCode();
            return response;

            //https://dccore.ntfy.pl/frontend/secure/bag/rate/5599234.json
            //{"data":"[{\"bagItem\":32604583,\"rate\":1,\"comment\":\"nie było\"},{\"bagItem\":32604584,\"rate\":5,\"comment\":\"\"},{\"bagItem\":32604585,\"rate\":5,\"comment\":\"\"},{\"bagItem\":32604586,\"rate\":3,\"comment\":\"\"},{\"bagItem\":32604587,\"rate\":3,\"comment\":\"\"}]"}
        }

        private StringContent GetJsonContent<T>(T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<T> GetResponse<T>(HttpResponseMessage response)
        {
            var responseText = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(responseText);
        }
    }
}
