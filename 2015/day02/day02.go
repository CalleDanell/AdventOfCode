package day02

import (
	"embed"
	"sort"
	"strconv"
	"strings"
)

//go:embed input.txt
var content embed.FS

// Run day two
func Run() (int, int) {
	input, _ := content.ReadFile("input.txt")
	strInput := strings.Split(string(input), "\n")

	totalpaper := 0
	totalribbon := 0
	for _, a := range strInput {
		parts := strings.Split(a, "x")
		len, _ := strconv.Atoi(parts[0])
		width, _ := strconv.Atoi(parts[1])
		height, _ := strconv.Atoi(strings.ReplaceAll(parts[2], "\r", "")) // Split leaves an empty space on last item

		side1 := (len * width)
		side2 := (width * height)
		side3 := (height * len)

		ribbon := []int{len, width, height}
		sides := []int{side1, side2, side3}

		sort.Ints(sides)
		sort.Ints(ribbon)

		totalpaper += (side1*2 + side2*2 + side3*2 + sides[0])
		totalribbon += ribbon[0] + ribbon[0] + ribbon[1] + ribbon[1] + (len * width * height)
	}

	part1 := totalpaper
	part2 := totalribbon

	return part1, part2
}
