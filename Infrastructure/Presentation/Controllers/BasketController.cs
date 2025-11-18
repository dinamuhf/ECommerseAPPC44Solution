using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.BasketModule;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.BasketModule;


namespace Persentation.Controllers
{
  
    public class basketController(IServiceManager _serviceManager) : APIBaseController
    {
        //Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(Basket);

        }
        
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreatedOrUpdatedBasketAsync(basket);
            return Ok(Basket);
        }


        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }
    }
}
