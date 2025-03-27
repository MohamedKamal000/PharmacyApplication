
using ApplicationLayer.Dtos.Delivery_DTOs;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer.Delivery_Handling
{
    public class DeliveryManHandler
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryManHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }


        public bool TrySearchDeliveryManWithPhone(string phoneNumber,out Delivery?  delivery)
        {
            delivery = _deliveryRepository.SearchDeliveryManByPhone(phoneNumber);

            return delivery != null;
        }

        public bool TrySearchDeliveryManWithName(string name, out Delivery? delivery)
        {
            delivery = _deliveryRepository.SearchDeliveryManByName(name);

            return delivery != null;
        }

        public bool TryDeleteDeliveryMan(string phoneNumber)
        {
            Delivery? delivery = _deliveryRepository.SearchDeliveryManByPhone(phoneNumber);
            if (delivery == null) return false;


            return _deliveryRepository.Delete(delivery) != -1;
        }

        public bool TryAddDeliveryMan(DeliveryDto delivery)
        {
            Delivery? d = _deliveryRepository.SearchDeliveryManByPhone(delivery.PhoneNumber);
            if (d != null) return false;


            return _deliveryRepository.Add(new Delivery()
            {
                DeliveryManName = delivery.DeliveryManName,
                PhoneNumber = delivery.PhoneNumber
            }) != -1;
        }

        public bool TryUpdateDeliveryMan(DeliveryDto delivery)
        {
            Delivery? d = _deliveryRepository.SearchDeliveryManByPhone(delivery.PhoneNumber);
            if (d == null) return false;

            d.PhoneNumber = delivery.PhoneNumber;
            d.DeliveryManName = delivery.DeliveryManName;

            return _deliveryRepository.Update(d) != -1;
        }
    }
}
