using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gishadev.gmtk.kids
{
    /// <summary>
    ///  Factory class to spawn kid prefabs.
    /// </summary>
    public class KidsFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly KidsDataSO _kidsData;

        public KidsFactory(IObjectResolver objectResolver, KidsDataSO kidsData)
        {
            _objectResolver = objectResolver;
            _kidsData = kidsData;
        }

        public Kid Create(Vector3 position)
        {
            return _objectResolver.Instantiate(_kidsData.KidPrefab, position, Quaternion.identity);
        }
    }
}
