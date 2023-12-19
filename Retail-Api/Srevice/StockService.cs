using Retail_Api.Models;
using Retail_Api.Repository;

namespace Retail_Api.Srevice
{
    public class StockService
    {
        private readonly StockRepositry _stockRepositry;

        public StockService(StockRepositry stockRepositry)
        {
            _stockRepositry = stockRepositry;
        }

        public IEnumerable<Stock> GetAll() =>_stockRepositry.GetAll();
        public Stock Add(StockDto stockDto)
        {
            var stock = new Stock()
            {
                Address = stockDto.Address,
                capacity = stockDto.capacity,
                Name = stockDto.Name,
            };
            _stockRepositry.Add(stock);
            save();
            return stock;
        }
        public void Edit(int id , StockDto stockDto)
        {
            var stock = new Stock()
            {
                Address = stockDto.Address,
                capacity = stockDto.capacity,
                Name = stockDto.Name,
                Id = id
            };
            _stockRepositry.Edit(stock);
            save();
        }
        public void Delete(int id) 
        {
            var stock = _stockRepositry.Get(id);
            _stockRepositry.Remove(stock);
            save();
        }
        public void save() => _stockRepositry.save();


    }
}
