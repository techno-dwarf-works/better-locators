using Better.Attributes.Runtime;
using Better.Locators.Runtime;
using UnityEngine;

namespace Samples.TestSamples
{
    public class TestGetService : MonoBehaviour
    {
        [EditorButton]
        private async void Test()
        {
            var t = await ServiceLocator.GetAsync<TestService>();
            Debug.Log(t.GetType().Name);
        }
    }
}