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

DIM=5
init x: DIM, y: DIM, z: DIM, size: 0.5, spacing: 0.2
while true
  (0..DIM-1).each do |x|
    (0..DIM-1).each do |y|
      (0..DIM-1).each do |z|
        color = "##{to_hex(rand(255))}#{to_hex(rand(255))}#{to_hex(rand(255))}"
        set id: "#{x}|#{y}|#{z}", color: color
      end
    end
  end
  wait 0.5
end
