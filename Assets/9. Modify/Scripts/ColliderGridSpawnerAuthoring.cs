// This script is used in the 5g1. Change Collision Filter - Boxes demo.
// Baker for the ColliderGridSpawner where the component is used in the ColliderGridCreationSystem.
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Unity.Physics
{
    public class ColliderGridSpawner : MonoBehaviour
    {
        public GameObject Prefab_BuiltInUniqueComponent;
        public GameObject Prefab_PhysicsShapeUniqueToggle;
        public GameObject Prefab_PhysicsShapeUniqueComponent;
        public UnityEngine.Material MaterialGrow;
        public UnityEngine.Material MaterialShrink;

        class ColliderGridSpawnerBaker : Baker<ColliderGridSpawner>
        {
            public override void Bake(ColliderGridSpawner authoring)
            {
                DependsOn(authoring.Prefab_BuiltInUniqueComponent);
                if (authoring.Prefab_BuiltInUniqueComponent == null || authoring.Prefab_PhysicsShapeUniqueToggle == null) return;

                var prefabBuiltInEntity = GetEntity(authoring.Prefab_BuiltInUniqueComponent, TransformUsageFlags.Dynamic);
                var prefabPhysicsShapeToggleEntity = GetEntity(authoring.Prefab_PhysicsShapeUniqueToggle, TransformUsageFlags.Dynamic);
                var prefabPhysicsShapeComponentEntity = GetEntity(authoring.Prefab_PhysicsShapeUniqueComponent, TransformUsageFlags.Dynamic);

                var materialGrow = authoring.MaterialGrow;
                var materialShrink = authoring.MaterialShrink;

                var createComponent = new CreateColliderGridComponent
                {
                    BuiltInEntity = prefabBuiltInEntity,
                    PhysicsShapeToggleEntity = prefabPhysicsShapeToggleEntity,
                    PhysicsShapeComponentEntity = prefabPhysicsShapeComponentEntity,
                    SpawningPosition = authoring.transform.position
                };
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, createComponent);

                AddSharedComponent(entity, new ColliderMaterialsComponent
                {
                    GrowMaterial = materialGrow,
                    ShrinkMaterial = materialShrink
                });
            }
        }
    }

    public struct CreateColliderGridComponent : IComponentData
    {
        public Entity BuiltInEntity;
        public Entity PhysicsShapeToggleEntity;
        public Entity PhysicsShapeComponentEntity;
        public float3 SpawningPosition;
    }

    public struct ColliderMaterialsComponent : ISharedComponentData
    {
        public UnityObjectRef<UnityEngine.Material> GrowMaterial;
        public UnityObjectRef<UnityEngine.Material> ShrinkMaterial;
    }
}
