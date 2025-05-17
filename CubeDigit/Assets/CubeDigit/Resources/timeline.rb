def init(x:, y:, z:, size:, spacing:)
  cmd :init, x: x, y: y, z: z, size: size, spacing: spacing
end

def set(id:, color:)
  cmd :set, id: id, color: color
end

def to_hex(n)
  n = [[n, 0].max, 255].min
  hex = n.to_i.to_s(16).upcase
  hex.size == 1 ? "0#{hex}" : hex
end

init x: 5, y: 5, z: 5, size: 0.5, spacing: 0.2
(0..4).each do |x|
  (0..4).each do |y|
    (0..4).each do |z|
      r = (x * 255 / 4.0).round
      g = (y * 255 / 4.0).round
      b = (z * 255 / 4.0).round
      color = "##{to_hex(r)}#{to_hex(g)}#{to_hex(b)}"
      set id: "#{x}|#{y}|#{z}", color: color
    end
  end
end
