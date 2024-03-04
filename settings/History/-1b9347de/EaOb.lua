---cn Only required if you have packer configured as `opt`
-- vim.cmd [[packadd packer.nvim]]
return require('packer').startup(function(use)
	use 'wbthomason/packer.nvim' -- this is essential.

	use 'navarasu/onedark.nvim'
	use 'bluz71/vim-moonfly-colors'


	use  {
		'neovim/nvim-lspconfig',
		requies = {
			'wiliamboman/mason.nvim',
			'wiliamboman/mason-lspconfig.nvim',
			'j-hui/fidget.nvim'
		}
	}

	use {
		'nvim-treesitter/nvim-treesitter',
		run = ':TSUpdate'
	}

	use 'nvim-tree/nvim-tree.lua'

	use {
		'nvim-telescope/telescope.nvim', tag = '0.1.5',
		-- or                            , branch = '0.1.x',
		requires = { {'nvim-lua/plenary.nvim'} }
	}

	use {
		's1n7ax/nvim-window-picker',
		tag = 'v2.*',
		config = function()
			require'window-picker'.setup()
		end,
	}

	use {
		'goolord/alpha-nvim',
		requires = { 'nvim-tree/nvim-web-devicons' },
		config = function ()
			require'alpha'.setup(require'alpha.themes.startify'.config)
		end
	}

	use{
		'nvim-lua/completion-nvim'
	}

	-- autocompletion
	use 'hrsh7th/cmp-nvim-lsp'
	use 'hrsh7th/cmp-nvim-lua'
	use 'hrsh7th/cmp-buffer'
	use 'hrsh7th/cmp-path'
	use 'hrsh7th/cmp-cmdline'
	use 'hrsh7th/cmp-vsnip'

	use {
		"hrsh7th/nvim-cmp",
		requires = {
			"hrsh7th/cmp-buffer", "hrsh7th/cmp-nvim-lsp",
			'quangnguyen30192/cmp-nvim-ultisnips', 'hrsh7th/cmp-nvim-lua',
			'octaltree/cmp-look', 'hrsh7th/cmp-path', 'hrsh7th/cmp-calc',
			'f3fora/cmp-spell', 'hrsh7th/cmp-emoji'
		}
	}

	use {
		'rafamadriz/friendly-snippets'
	}

	use {
		"L3MON4D3/LuaSnip",
		-- follow latest release.
		tag = "v2.*", -- Replace <CurrentMajor> by the latest released major (first number of latest release)
		-- install jsregexp (optional!:).
		dependencies = {
			"saadparwaiz1/cmp_luasnip",
			"rafamadriz/friendly-snippets"
		}
	}

--	use {
--		'nvim-orgmode/orgmode', 
--		config = function()
--			require('orgmode').setup{}
--	end
--	}

	use 'ggandor/leap.nvim'
	--use {
		--	"stevearc/oil.nvim",
		--	config = function()
			--		require("oil").setup()
			--	end
			--}

			use({
				"epwalsh/obsidian.nvim",
				tag = "*",  -- recommended, use latest release instead of latest commit
				requires = {
					"nvim-lua/plenary.nvim",
				}
			})

			use 'chentoast/marks.nvim'

			use { 'chomosuke/term-edit.nvim', tag = 'v1.*' }

			use 'xorid/swap-split.nvim'

--			use "nvim-orgmode/orgmode"	

			use 'ThePrimeagen/vim-be-good'


		end)
