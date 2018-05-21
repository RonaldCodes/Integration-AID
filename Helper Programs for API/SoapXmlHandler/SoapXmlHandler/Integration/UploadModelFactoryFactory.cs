using Agent.Exceptions;
using System;
using System.Collections.Generic;

namespace Agent.Integration
{
    public class UploadModelFactoryFactory
    {
        private readonly Dictionary<string, Func<IUploadModelFactory>> _factories;

        public UploadModelFactoryFactory()
        {
            _factories = new Dictionary<string, Func<IUploadModelFactory>>
            {
                {"331", () => new RttStyleConsolidationModelFactory() }, //Style JHB
                {"334", () => new RttStyleConsolidationModelFactory() }, //Style CPT
                {"335", () => new DefaultUploadModelFactory() }, //KZN (Style)
                {"336", () => new DefaultUploadModelFactory() }, //Bloem
                {"337", () => new DefaultUploadModelFactory() }, //East London
                {"338", () => new DefaultUploadModelFactory() }, //George
                {"340", () => new DefaultUploadModelFactory() }, //Mozambique
                {"341", () => new DefaultUploadModelFactory() }, //Botswana
                {"342", () => new DefaultUploadModelFactory() }, //Zambia
                {"343", () => new DefaultUploadModelFactory() }, //Namibia
                {"344", () => new DefaultUploadModelFactory() }, //Swaziland
                {"345", () => new DefaultUploadModelFactory() }, //Lesotho
                {"346", () => new DefaultUploadModelFactory() }, //Zimbabwe
                {"347", () => new DefaultUploadModelFactory() }, //Polokwane
                {"348", () => new DefaultUploadModelFactory() }, //Malawi
                {"351", () => new RttTarsusModelFactory() }, //Tarsus
                {"378", () => new RttTarsusModelFactory() }, //IHS
                {"110", () => new RttIHSModelFactory() }, //Trackmatic Test
                {"394", () => new RttTarsusModelFactory() }, //Jet Park
                {"396", () => new RttIHSModelFactory() }, //Durban
                {"401", () => new RttIHSModelFactory() }, //Cape Town
                {"339", () => new RttTarsusModelFactory() }, //Port Elizabeth
            };
        }

        public IUploadModelFactory Create(string id)
        {
            if(!_factories.ContainsKey(id))
            {
                throw new NoFactoryException($"An upload model factory has not been defined for {id}");
            }
            return _factories[id]();
        }
    }
}
