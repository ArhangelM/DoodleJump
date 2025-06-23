using Assets.DoodleJump.Scripts.Runtime.Interaction.Platforms;
using Assets.DoodleJump.Scripts.Storage.SpawnConfig;
using System.Collections.Generic;
using System.Linq;
using Tools.Extensions;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Runtime.Location
{
    public class LocationGenerator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _platformPrefabs;
        [SerializeField] private List<SpawnConfiguration> _spawnConfigurations;

        [SerializeField] private Transform _platformParant;
        [SerializeField] private int _initialPlatforms = 15;
        [SerializeField] private float _maxDistanceBetweenPlatforms = 1.5f;
        [SerializeField] private float _minDistanceBetweenPlatforms = 0.3f;

        private Transform _playerTransform;
        private Queue<GameObject> _platforms = new();

        private float _highestY = -4f;
        private float _screenLeftX;
        private float _screenRightX;

        private void Update()
        {
            if (_playerTransform.HasValue())
            {
                if (_playerTransform.position.y + 5f > _highestY)
                {
                    GeneratePlatformRow();
                    ClenupPlatformsRow();
                }
            }
        }

        private void GeneratePlatformRow()
        {
            float x = Random.Range(_screenLeftX + 0.25f, _screenRightX - 0.25f);
            Vector3 position = new Vector3(x, _highestY, 0f);

            var platform = Instantiate(_platformPrefabs[Random.Range(0, _platformPrefabs.Count)], position, Quaternion.identity);
            platform.transform.SetParent(_platformParant);
            _platforms.Enqueue(platform);

            RandomSpawn(platform.GetComponent<BasePlatform>());

            float distance = Random.Range(_minDistanceBetweenPlatforms, _maxDistanceBetweenPlatforms);
            _highestY += distance;
        }

        private void ClenupPlatformsRow()
        {
            if (_platforms.Count > 0)
            {
                GameObject element = _platforms.Dequeue();
                Destroy(element);
            }
        }

        private void RandomSpawn(BasePlatform platform)
        {
            if (_spawnConfigurations.Count == 0)
                return;

            float sumChance = _spawnConfigurations.Sum(config => config.Chance);
            float randomValue = Random.Range(0, sumChance * 3);
            float sum = 0;

            for (int i = 0; i < _spawnConfigurations.Count; i++)
            {
                sum += _spawnConfigurations[i].Chance;
                if (randomValue < sum)
                {
                    platform.Spawn(_spawnConfigurations[i].Prefab);
                    return;
                }
            }
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public void ResetGenerator()
        {
            _highestY = -4f;
            _platforms.Clear();
            foreach (Transform child in _platformParant)
            {
                Destroy(child.gameObject);
            }
            GenerateInitialPlatforms();
        }

        public void DisplaySetup()
        {
            var camera = Camera.main;
            Vector3 leftPoint = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector3 rightPoint = camera.ViewportToWorldPoint(new Vector3(1, 0, 0));

            _screenLeftX = leftPoint.x;
            _screenRightX = rightPoint.x;
        }

        public void GenerateInitialPlatforms()
        {
            for (int i = 0; i < _initialPlatforms; i++)
            {
                GeneratePlatformRow();
            }
        }
    }
}