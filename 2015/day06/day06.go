package day06

import (
	"embed"
	"strconv"
	"strings"
)

//go:embed input.txt
var content embed.FS

type Coordinate struct {
	x int
	y int
}

// Run day five
func Run() (int, int) {
	input, _ := content.ReadFile("input.txt")
	lines := strings.Split(string(input), "\n")

	m := make(map[Coordinate]int)
	m2 := make(map[Coordinate]int)

	for _, line := range lines {
		line = strings.ReplaceAll(line, "\r", "")
		parts := strings.Split(line, " ")
		startX, startY, endX, endY := 0, 0, 0, 0
		if strings.Contains(line, "toggle") {
			startX, startY = GetStartEnd(parts[1])
			endX, endY = GetStartEnd(parts[3])
			Execute("toggle", startX, endX, startY, endY, m)
			ExecuteTwo("toggle", startX, endX, startY, endY, m2)
		} else if strings.Contains(line, "turn on") {
			startX, startY = GetStartEnd(parts[2])
			endX, endY = GetStartEnd(parts[4])
			Execute("on", startX, endX, startY, endY, m)
			ExecuteTwo("on", startX, endX, startY, endY, m2)
		} else {
			startX, startY = GetStartEnd(parts[2])
			endX, endY = GetStartEnd(parts[4])
			Execute("off", startX, endX, startY, endY, m)
			ExecuteTwo("off", startX, endX, startY, endY, m2)
		}
	}

	lit := 0
	for element := range m {
		if m[element] == 1 {
			lit++
		}
	}

	brightness := 0
	for element := range m2 {
		brightness += m2[element]
	}

	part1 := lit
	part2 := brightness

	return part1, part2
}

func Execute(command string, startX int, endX int, startY int, endY int, m map[Coordinate]int) {
	for i := startX; i <= endX; i++ {
		for j := startY; j <= endY; j++ {
			coord := Coordinate{i, j}
			if command == "on" {
				m[coord] = 1
			} else if command == "off" {
				m[coord] = 0
			} else {
				if value, ok := m[coord]; ok {
					if value == 0 {
						m[coord] = 1
					} else {
						m[coord] = 0
					}
				} else {
					m[coord] = 1
				}
			}
		}
	}
}

func ExecuteTwo(command string, startX int, endX int, startY int, endY int, m map[Coordinate]int) {
	for i := startX; i <= endX; i++ {
		for j := startY; j <= endY; j++ {
			coord := Coordinate{i, j}
			if command == "on" {
				m[coord]++
			} else if command == "off" {
				if m[coord] > 0 {
					m[coord]--
				}
			} else {
				m[coord] += 2
			}
		}
	}
}

func GetStartEnd(s string) (int, int) {
	parts := strings.Split(s, ",")
	start, _ := strconv.Atoi(parts[0])
	end, _ := strconv.Atoi(parts[1])

	return start, end
}
