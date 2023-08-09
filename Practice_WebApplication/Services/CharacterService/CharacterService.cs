namespace Practice_WebApplication.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);

            _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();  

            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacters = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {

                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                if (character == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Character with Id '{updateCharacter.Id}' not found.";
                    return serviceResponse;
                }

                _mapper.Map(updateCharacter, character);

                character.Name = updateCharacter.Name;
                character.Class = updateCharacter.Class;
                character.defense = updateCharacter.defense;
                character.hitPoints = updateCharacter.hitPoints;
                character.intelligence = updateCharacter.intelligence;
                character.strength = updateCharacter.strength;

                await _context.SaveChangesAsync();
                
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

                if (character == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Character with Id '{id}' not found.";
                    return serviceResponse;
                }
                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();
               
                serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
