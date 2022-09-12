using System.Diagnostics.CodeAnalysis;

namespace Content.Shared.Construction
{
    [Serializable]
    [DataDefinition]
    public sealed class ConstructionGraphNode
    {
        [DataField("actions", serverOnly: true)]
        private IGraphAction[] _constructionActions = Array.Empty<IGraphAction>();

        [DataField("spawned", serverOnly: true)]
        private IGraphAction[] _spawnActions = Array.Empty<IGraphAction>();

        [DataField("edges")]
        private ConstructionGraphEdge[] _edges = Array.Empty<ConstructionGraphEdge>();

        [ViewVariables]
        [DataField("node", required: true)]
        public string Name { get; private set; } = default!;

        [ViewVariables]
        public IReadOnlyList<ConstructionGraphEdge> Edges => _edges;

        [ViewVariables]
        public IReadOnlyList<IGraphAction> SpawnActions => _spawnActions;

        [ViewVariables]
        public IReadOnlyList<IGraphAction> ConstructionActions => _constructionActions;

        [ViewVariables]
        [DataField("entity")]
        public string? Entity { get; private set; }

        public ConstructionGraphEdge? GetEdge(string target)
        {
            foreach (var edge in _edges)
            {
                if (edge.Target == target)
                    return edge;
            }

            return null;
        }

        public int? GetEdgeIndex(string target)
        {
            for (var i = 0; i < _edges.Length; i++)
            {
                var edge = _edges[i];
                if (edge.Target == target)
                    return i;
            }

            return null;
        }

        public bool TryGetEdge(string target, [NotNullWhen(true)] out ConstructionGraphEdge? edge)
        {
            return (edge = GetEdge(target)) != null;
        }
    }
}
