using Content.Server.Construction.Components;
using Content.Shared.Access.Components;
using Content.Shared.Construction;
using JetBrains.Annotations;
using Robust.Server.Containers;
using Robust.Shared.Containers;

namespace Content.Server.Construction.Completions
{
    [UsedImplicitly]
    [DataDefinition]
    public sealed class UpdateAccess : IGraphAction
    {
        public void PerformAction(EntityUid uid, EntityUid? userUid, IEntityManager entityManager)
        {
            var accessComp = entityManager.EnsureComponent<AccessReaderComponent>(uid);

            // Ensure that the construction component is aware of the board container.
            if (entityManager.TryGetComponent(uid, out ConstructionComponent? construction))
            {
                entityManager.TrySystem(out ConstructionSystem? constructionSystem);
                if (constructionSystem != null)
                    constructionSystem.AddContainer(uid, "board", construction);
            }

            /* We don't do anything if this is null or empty.
            if (string.IsNullOrEmpty(door.BoardPrototype))
                return;
            */

            if (entityManager.TrySystem(out ContainerSystem? containerSystem))
            {
                if (containerSystem != null)
                {
                    var container = containerSystem.EnsureContainer<Container>(uid, "board", out var existed);
                    if (container.ContainedEntities.Count > 0)
                    {
                        if (entityManager.TryGetComponent(container.ContainedEntities[0], out AccessReaderComponent? access))
                        {
                            accessComp.AccessLists.Clear();
                            accessComp.AccessLists.AddRange(access.AccessLists);
                        }
                    }
                }
            }
        }
    }
}
