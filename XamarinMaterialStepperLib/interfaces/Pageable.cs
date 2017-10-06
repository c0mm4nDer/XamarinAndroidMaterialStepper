using System.Collections.Generic;

namespace XamarinMaterialStepperLib.interfaces
{
    public interface Pageable
    {
        void add(AbstractStep fragment);

        void set(List<AbstractStep> fragments);
    }
}