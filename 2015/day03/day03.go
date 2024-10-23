package day03

import (
	"embed"
)

//go:embed input.txt
var content embed.FS

type Coordinate struct {
	x int
	y int
}

var current *Coordinate

func PartOne(strInput string, m map[Coordinate]int) int {
	current := &Coordinate{0, 0}
	m[Coordinate{0, 0}] = 1

	for _, ch := range strInput {
		if ch == '<' {
			current.x -= 1
		} else if ch == '>' {
			current.x += 1
		} else if ch == '^' {
			current.y += 1
		} else {
			current.y -= 1
		}

		_, ok := m[*current]
		if !ok {
			m[*current] = 1
		} else {
			m[*current]++
		}
	}

	frequentlyVisitedHouses := 0
	for _, element := range m {
		if element > 0 {
			frequentlyVisitedHouses++
		}
	}

	return frequentlyVisitedHouses
}

func PartTwo(strInput string, m map[Coordinate]int) int {
	current1 := &Coordinate{0, 0}
	current2 := &Coordinate{0, 0}
	m[Coordinate{0, 0}] = 2

	for i, ch := range strInput {
		if i%2 == 0 {
			current = current1
		} else {
			current = current2
		}

		if ch == '<' {
			current.x -= 1
		} else if ch == '>' {
			current.x += 1
		} else if ch == '^' {
			current.y += 1
		} else {
			current.y -= 1
		}

		_, ok := m[*current]
		if !ok {
			m[*current] = 1
		} else {
			m[*current]++
		}
	}

	frequentlyVisitedHouses := 0
	for _, element := range m {
		if element > 0 {
			frequentlyVisitedHouses++
		}
	}

	return frequentlyVisitedHouses
}

// Run day three
func Run() (int, int) {
	input, _ := content.ReadFile("input.txt")
	strInput := string(input)

	m := make(map[Coordinate]int)

	part1 := PartOne(strInput, m)
	part2 := PartTwo(strInput, m)

	return part1, part2
}
