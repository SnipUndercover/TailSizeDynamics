--- @class EntityHandler<Trigger>
local configureTailSizeDynamicsTrigger = {}

local scaleMethods = {
  ['Off'] = 0,
  ['Additive'] = 1,
  ['Accumulative'] = 2
}

local timeUnits = {
  ['Frames'] = 1,
  ['Seconds'] = 60
}

local mapProgressionModes = {
  ['Visited Rooms'] = 0,
  ['Completed Maps'] = 1
}

local minigameResetModes = {
  ['Return to Map'] = 0,
  ['Restart Chapter'] = 1
}

configureTailSizeDynamicsTrigger.name = 'TailSizeDynamics/ConfigureTailSizeDynamicsTrigger'
configureTailSizeDynamicsTrigger.fieldInformation = {
  scaleMethod = {
    options = scaleMethods,
    editable = false
  },
  timeScaleUnit = {
    options = timeUnits,
    editable = false
  },
  mapProgressionScaleMode = {
    options = mapProgressionModes,
    editable = false
  },
  minigameResetMode = {
    options = minigameResetModes,
    editable = false
  }
}

configureTailSizeDynamicsTrigger.fieldOrder = {
  'x', 'y',
  'width', 'height',
  'baseScale', 'scaleMethod',
  'crystalHeartScaleMultiplier', 'cassetteScaleMultiplier',
  'summitGemScaleMultiplier', 'strawberryScaleMultiplier',
  'goldenStrawberryScaleMultiplier', 'wingedGoldenScaleMultiplier',
  'moonberryScaleMultiplier', 'deathScaleMultiplier',
  'dashScaleMultiplier', 'jumpScaleMultiplier',
  'timeScaleMultiplier', 'timeScaleUnit',
  'mapProgressionScaleMultiplier', 'mapProgressionScaleMode',
  'minigameResetMode', 'minigameMode'
}

configureTailSizeDynamicsTrigger.ignoredFields = {
  '_version'
}

configureTailSizeDynamicsTrigger.placements = {
  name = 'normal',
  data = {
    _version = 1,
    scaleMethod = 1,
    crystalHeartScaleMultiplier = 0,
    cassetteScaleMultiplier = 0,
    summitGemScaleMultiplier = 0,
    strawberryScaleMultiplier = 0,
    goldenStrawberryScaleMultiplier = 0,
    wingedGoldenScaleMultiplier = 0,
    moonberryScaleMultiplier = 0,
    deathScaleMultiplier = 0,
    dashScaleMultiplier = 0,
    jumpScaleMultiplier = 0,
    timeScaleMultiplier = 0,
    timeScaleUnit = 60,
    mapProgressionScaleMultiplier = 0,
    mapProgressionScaleMode = 0,
    baseScale = 1,
    minigameMode = false,
    minigameResetMode = 0
  }
}

return configureTailSizeDynamicsTrigger
