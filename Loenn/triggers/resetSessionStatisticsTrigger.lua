--- @class EntityHandler<Trigger>
local resetSessionStatisticsTrigger = {}
resetSessionStatisticsTrigger.name = 'TailSizeDynamics/ResetSessionStatisticsTrigger'
resetSessionStatisticsTrigger.fieldOrder = {
  'x', 'y',
  'width', 'height',
  'crystalHearts', 'cassettes', 'summitGems', 'strawberries',
  'goldenStrawberries', 'wingedGoldens', 'moonberries', 'deaths',
  'dashes', 'jumps', 'time', 'mapProgression',
  'accumulatedScale'
}

resetSessionStatisticsTrigger.ignoredFields = {
  '_version'
}

resetSessionStatisticsTrigger.placements = {
  name = 'normal',
  data = {
    _version = 1,
    crystalHearts = true,
    cassettes = true,
    summitGems = true,
    strawberries = true,
    goldenStrawberries = true,
    wingedGoldens = true,
    moonberries = true,
    deaths = true,
    dashes = true,
    jumps = true,
    time = true,
    mapProgression = true,
    accumulatedScale = true
  }
}

return resetSessionStatisticsTrigger
