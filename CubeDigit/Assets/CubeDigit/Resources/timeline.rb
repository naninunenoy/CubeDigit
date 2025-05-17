def init(x:, y:, z:, size:, spacing:)
  cmd :init, x: x, y: y, z: z, size: size, spacing: spacing
end

def set(id:, color:)
  cmd :set, id: id, color: color
end

init x: 3, y: 3, z: 3, size: 1, spacing: 0.15
set id: "0|0|0", color: "#AABBCC"
