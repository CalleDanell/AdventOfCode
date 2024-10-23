package day07_test

import (
	"embed"
	"fmt"
	"strings"
)

//go:embed input.txt
var content embed.FS

// Run day five
func Run() (int, int) {
	input, _ := content.ReadFile("input.txt")
	lines := strings.Split(string(input), "\n")

	for _, line := range lines {
		line = strings.ReplaceAll(line, "\r", "")
		fmt.Println(line)
	}

	return 0, 0
}
