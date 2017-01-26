using FileImportProcessingUsingBusDefer.Data;
using NServiceBus.Features;

namespace FileImportProcessingUsingBusDefer.FileImportInsertionEndpoint
{
    //https://docs.particular.net/nservicebus/pipeline/features
    public class EnsureTablesAreCreatedWhenConfiguringEndpoint : Feature
    {
        //always called
        public EnsureTablesAreCreatedWhenConfiguringEndpoint()
        {
            using (var dbContext = new FileImportContext())
            {
                dbContext.Database.Initialize(false);
            }
        }

        //called if all defined conditions are met and the feature is marked as enabled
        //Use this method to configure and initialize all required components for the feature.
        protected override void Setup(FeatureConfigurationContext context)
        {
        }
    }
}
